namespace CampainScheduler.DAL.Seeders
{
    internal static class TemplateSeedIdResolver
    {
        public static int GetTemplateId(string templateName)
        {
            if (templateName.Contains(TemplateSeeder.TEMPLATE_A_NAME))
            {
                return 1;
            }
            if (templateName.Contains(TemplateSeeder.TEMPLATE_B_NAME))
            {
                return 2;
            }
            if (templateName.Contains(TemplateSeeder.TEMPLATE_C_NAME))
            {
                return 3;
            }

            return 0;
        }
    }
}