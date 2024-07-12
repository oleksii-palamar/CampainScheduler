using CampainScheduler.Application.CampainService;
using CampainScheduler.Application.CampainService.Interfaces;
using CampainScheduler.Application.Sender;
using CampainScheduler.Application.Sender.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CampainScheduler.Application
{
    public static class CampainSchedulerApplicationRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .RegisterDependencies();
        }

        public static IApplicationBuilder SetupApplication(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();

                var sendsFileFullPath = $"./{configuration["CampainSender:OutputFile"]}";

                File.WriteAllText(sendsFileFullPath, string.Empty);
            }

            return app;
        }

        private static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICampainConditionResolver, CampainConditionResolver>();
            services.AddScoped<ICampainService, CampainService.CampainService>();
            services.AddScoped<ICampainSender, ToFileCampainSender>();

            return services;
        }
    }
}
