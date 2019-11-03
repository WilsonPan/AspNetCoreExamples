using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Razor.Filters
{
    public class GlobalPageFilter : IPageFilter
    {
        private readonly ILogger _logger;

        public GlobalPageFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            _logger.LogDebug("Global Filter OnPageHandlerSelected called.");
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            _logger.LogDebug("Global Filter OnPageHandlerExecuting called.");
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            _logger.LogDebug("Global Filter OnPageHandlerSelected called.");
        }
    }
}