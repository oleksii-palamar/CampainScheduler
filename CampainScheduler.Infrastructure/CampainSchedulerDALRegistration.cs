using CampainScheduler.Application.Context.Interfaces;
using CampainScheduler.DAL.Contexts;
using CampainScheduler.DAL.Seeders;
using CampainScheduler.DAL.Seeders.Interfaces;
using CampainScheduler.Domain.Models;
using CampainScheduler.Utils.CsvParsers;
using CampainScheduler.Utils.CsvParsers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CampainScheduler.DAL
{
    public static class CampainSchedulerDALRegistration
    {
        public static IServiceCollection AddDAL(this IServiceCollection services)
        {
            services.AddDbContext<CampainSchedulerContext>();

            return services
                .RegisterDependencies();
        }

        public static IApplicationBuilder SetupDAL(this IApplicationBuilder app)
        {
            // Seeding inmemory database
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CampainSchedulerContext>();
                context?.Database.EnsureCreated();
            }

            return app;
        }

        private static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICampainSchedulerContext, CampainSchedulerContext>();

            services.AddKeyedScoped<IEntitySeeder<Customer>, CustomerSeeder>("customerSeeder");
            services.AddKeyedScoped<IEntitySeeder<Template>, TemplateSeeder>("templateSeeder");
            services.AddKeyedScoped<IEntitySeeder<Campain>, CampainSeeder>("campainSeeder");
            services.AddKeyedScoped<ICsvParser<Customer>, CustomerCsvParser>("customerCsvParser");

            return services;
        }
    }
}
