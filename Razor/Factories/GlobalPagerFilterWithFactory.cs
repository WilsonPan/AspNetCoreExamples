using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Razor.Factories
{
    public class GlobalPagerFilterWithFactory : IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetService<ILogger<Filters.GlobalPageFilter>>();

            return new Filters.GlobalPageFilter(logger);
        }
    }
}