using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Data.EFCoreDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, Data.EFCoreDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            var student = _context.Student.Single(m => m.Id == 1);
            student.Age = 32;

            //模拟另外一个用户修改了Age
            var task = Task.Run(() =>
            {
                var options = HttpContext.RequestServices.GetService<DbContextOptions<Data.EFCoreDbContext>>();
                using (var context = new Data.EFCoreDbContext(options))
                {
                    var student = context.Student.Single(m => m.Id == 1);
                    student.Age = 23;
                    context.SaveChanges();
                }
            });
            task.Wait();
            //到这，另外一个线程已经将Age修改成23

            var trySave = 0;

            //若并发冲突异常，重试3次
            while (trySave++ < 3)
            {
                if (TrySaveData()) break;
            }

            bool TrySaveData()
            {
                try
                {
                    _context.SaveChanges();
                    return true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, $"database update concurrency exception : retry: {trySave}");

                    //3次尝试保存失败，抛出异常等上层处理，不应该吃掉异常，不然返回成功，实际保存没成功
                    if (trySave >= 3) throw ex;

                    //若冲突不是当前处理的对象，抛出异常等上层处理
                    if (!ex.Entries.Any(m => m.Entity is Models.Student)) throw ex;

                    var entry = ex.Entries.Select(m => m).Single(m => m.Entity is Models.Student);
                    //获取当前实体值
                    var currentValues = entry.CurrentValues;
                    //获取数据库值
                    var databaseValues = entry.GetDatabaseValues();

                    //这里获取当前需要修改的字段
                    var property = currentValues.Properties.FirstOrDefault(m => m.Name == nameof(student.Age));
                    var currentValue = currentValues[property];
                    var databaseValue = databaseValues[property];

                    //这里赋值多个选择方案，1. 使用当前值 2. 使用数据库值 3. 处理后的值（例如余额，数据库余额 - 当前余额 & 大于0)
                    currentValues[property] = currentValue;

                    // 刷新原始值，这里原始值是做并发检查
                    entry.OriginalValues.SetValues(databaseValues);

                    return false;
                }
            }
        }
    }
}
