using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetcoreapp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using aspnetcoreapp.Services;
using Microsoft.AspNetCore.Http;

namespace aspnetcoreapp.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(ILogger<IndexModel> logger,
                          IHttpContextAccessor accessor)
        {
            var service = accessor.HttpContext.RequestServices.GetService<IMyService>();
            service.Run();
        }

        public void OnGet()
        {

        }
    }
}
