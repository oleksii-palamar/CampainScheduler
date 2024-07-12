using CampainScheduler.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampainScheduler.DAL.ModelConfigurations
{
    internal class CampainEntityTypeConfiguration : IEntityTypeConfiguration<Campain>
    {
        public void Configure(EntityTypeBuilder<Campain> builder)
        {
            builder
                .Property(x => x.Id)
                .HasColumnName("CAMPAIN_ID");
        }
    }
}
