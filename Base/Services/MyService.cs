using Microsoft.Extensions.Logging;

namespace aspnetcoreapp.Services
{
    public class MyService : IMyService
    {
        private readonly ILogger<MyService> _logger;
        private readonly IServicesTransient _servicesTransient;
        public MyService(ILogger<MyService> logger,
                         IServicesTransient servicesTransient)
        {
            _logger = logger;
            _servicesTransient = servicesTransient;
        }

        public void Run()
        {
            _logger.LogInformation($"IServicesTransient : {_servicesTransient.GetHashCode()}");
        }
    }
}