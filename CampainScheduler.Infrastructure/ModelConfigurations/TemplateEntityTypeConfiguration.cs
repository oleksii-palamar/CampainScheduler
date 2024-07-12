using CampainScheduler.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampainScheduler.DAL.ModelConfigurations
{
    internal class TemplateEntityTypeConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder
                .Property(x => x.Id)
                .HasColumnName("TEMPLATE_ID");
        }
    }
}
