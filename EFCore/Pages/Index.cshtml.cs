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

            #region 模拟另外一个用户修改了Age

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

            #endregion

            var (trySave, isSave) = (0, false);

            while (!isSave && trySave++ < 3)
            {
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "database update error");

                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is Models.Student)
                        {
                            var currentValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in currentValues.Properties)
                            {
                                var currentValue = currentValues[property];
                                var databaseValue = databaseValues[property];

                                //这里选择保存哪个值，这里简单选择当前（30）保存到数据库，实际可能还需处理，如余额，就需要数据库当前余额 - 当前数值
                                currentValues[property] = currentValue;
                            }
                            // 刷新原始值
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                    }
                }
            }
        }
    }
}
