using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Data.RazorDbContext _dbContext;
        public IndexModel(ILogger<IndexModel> logger,
                          Data.RazorDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        //在选择处理程序方法后但在模型绑定发生前调用
public override void OnPageHandlerSelected(Microsoft.AspNetCore.Mvc.Filters.PageHandlerSelectedContext context)
{
    _logger.LogDebug("OnPageHandlerSelected");
}
//在执行处理器方法前，模型绑定完成后调用
public override void OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext context)
{
    _logger.LogDebug("OnPageHandlerExecuting");
}
//在执行处理器方法后，生成操作结果前调用
public override void OnPageHandlerExecuted(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutedContext context)
{
    _logger.LogDebug("OnPageHandlerExecuted");
}

        public List<Models.Book> Books { get; set; }

        public IActionResult OnGet()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            Books = _dbContext.Book
                                .AsNoTracking()
                                .ToList();
            return Page();
        }
    }
}
