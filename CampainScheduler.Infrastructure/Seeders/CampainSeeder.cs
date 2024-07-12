using CampainScheduler.DAL.Seeders.Interfaces;
using CampainScheduler.Domain.Models;

namespace CampainScheduler.DAL.Seeders
{
    public class CampainSeeder : IEntitySeeder<Campain>
    {
        public List<Campain> GetEntities() =>
            new List<Campain>
            {
                new Campain
                {
                    Id = 1,
                    CustomersCondition = "To all the Male customers",
                    Priority = 1,
                    ScheduledTimeUtc = new TimeSpan(10, 15, 0),
                    TemplateId = TemplateSeedIdResolver.GetTemplateId(TemplateSeeder.TEMPLATE_A_NAME),
                },
                new Campain
                {
                    Id = 2,
                    CustomersCondition = "To all customers above the age 45 ",
                    Priority = 2,
                    ScheduledTimeUtc = new TimeSpan(10, 5, 0),
                    TemplateId = TemplateSeedIdResolver.GetTemplateId(TemplateSeeder.TEMPLATE_B_NAME),
                },
                new Campain
                {
                    Id = 3,
                    CustomersCondition = "To all the customers from New York ",
                    Priority = 5,
                    ScheduledTimeUtc = new TimeSpan(10, 10, 0),
                    TemplateId = TemplateSeedIdResolver.GetTemplateId(TemplateSeeder.TEMPLATE_C_NAME),
                },
                new Campain
                {
                    Id = 4,
                    CustomersCondition = "To all the customers that deposit more than 100 $",
                    Priority = 3,
                    ScheduledTimeUtc = new TimeSpan(10, 15, 0),
                    TemplateId = TemplateSeedIdResolver.GetTemplateId(TemplateSeeder.TEMPLATE_A_NAME),
                },
                new Campain
                {
                    Id = 5,
                    CustomersCondition = "To all the customers that marked as new Customers",
                    Priority = 4,
                    ScheduledTimeUtc = new TimeSpan(10, 5, 0),
                    TemplateId = TemplateSeedIdResolver.GetTemplateId(TemplateSeeder.TEMPLATE_C_NAME),
                },
            };
    }
}
