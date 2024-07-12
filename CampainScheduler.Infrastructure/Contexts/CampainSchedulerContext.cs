using Microsoft.EntityFrameworkCore;
using CampainScheduler.Domain.Models;
using CampainScheduler.DAL.ModelConfigurations;
using Microsoft.Extensions.DependencyInjection;
using CampainScheduler.DAL.Seeders.Interfaces;
using CampainScheduler.Application.Context.Interfaces;

namespace CampainScheduler.DAL.Contexts
{
    public class CampainSchedulerContext : DbContext, ICampainSchedulerContext
    {
        private readonly IEntitySeeder<Customer> _customerEntitySeeder;
        private readonly IEntitySeeder<Template> _templateEntitySeeder;
        private readonly IEntitySeeder<Campain> _campaintEntitySeeder;

        public CampainSchedulerContext(
            [FromKeyedServices("customerSeeder")] IEntitySeeder<Customer> customerEntitySeeder,
            [FromKeyedServices("templateSeeder")] IEntitySeeder<Template> templateEntitySeeder,
            [FromKeyedServices("campainSeeder")] IEntitySeeder<Campain> campaintEntitySeeder)
        {
            _customerEntitySeeder = customerEntitySeeder;
            _templateEntitySeeder = templateEntitySeeder;
            _campaintEntitySeeder = campaintEntitySeeder;
        }

        public DbSet<Campain> Campains { get; set; }

        public DbSet<Template> Templates { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CampainEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TemplateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());

            modelBuilder.Entity<Customer>().HasData(_customerEntitySeeder.GetEntities());
            modelBuilder.Entity<Template>().HasData(_templateEntitySeeder.GetEntities());
            modelBuilder.Entity<Campain>().HasData(_campaintEntitySeeder.GetEntities());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoDb");
        }
    }
}
