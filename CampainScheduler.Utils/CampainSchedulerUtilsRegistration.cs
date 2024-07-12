using CampainScheduler.Application.Scheduler.Interfaces;
using CampainScheduler.Application.Sender.Interfaces;
using CampainScheduler.Utils.Cron;
using CampainScheduler.Utils.Cron.Interfaces;
using CampainScheduler.Utils.FileWritter;
using CampainScheduler.Utils.Schedulers;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace CampainScheduler.Utils
{
    public static class CampainSchedulerUtilsRegistration
    {
        public static IServiceCollection AddUtils(this IServiceCollection services)
        {
            services.AddHangfire(configuration => configuration
                .UseInMemoryStorage()
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings());
            services.AddHangfireServer();

            return services
                .RegisterDependencies();
        }

        private static IServiceCollection RegisterDependencies(this IServiceCollection services) 
        {
            services.AddScoped<ICampainScheduler, HangfireCampainScheduler>();
            services.AddScoped<ICronConvertor, CronConvertor>();
            services.AddScoped<IFileWritter, SimpleFileWritter>();

            return services;
        }
    }
}
