using LocalizaLabs.Domain.Interfaces.Services;
using LocalizaLabs.Domain.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizaLabs.Infrastructure.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            #region Domain - Services
            services.AddScoped<INumberProcessingService, NumberProcessingService>();
            #endregion
        }
    }
}
