using CampainScheduler.DAL.Seeders.Interfaces;
using CampainScheduler.Domain.Models;

namespace CampainScheduler.DAL.Seeders
{
    public class TemplateSeeder : IEntitySeeder<Template>
    {
        private const string TemplateFolder = @"../InitialData/Templates";

        public const string TEMPLATE_A_NAME = "TemplateA";
        public const string TEMPLATE_B_NAME = "Template B";
        public const string TEMPLATE_C_NAME = "Template C";

        public List<Template> GetEntities()
        {
            var templateFiles = Directory.GetFiles(TemplateFolder, "*.html");

            return templateFiles
                .Select((f, i) => new Template
                {
                    Id = TemplateSeedIdResolver.GetTemplateId(f),
                    TemplateBody = File.ReadAllText(f),
                })
                .ToList();
        }
    }
}
