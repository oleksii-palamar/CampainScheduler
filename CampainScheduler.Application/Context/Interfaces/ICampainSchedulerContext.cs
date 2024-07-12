using CampainScheduler.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CampainScheduler.Application.Context.Interfaces
{
    public interface ICampainSchedulerContext
    {
        DbSet<Campain> Campains { get; }
        DbSet<Template> Templates { get; }
        DbSet<Customer> Customers { get; }
    }
}
