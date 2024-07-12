using CampainScheduler.DAL.Seeders.Interfaces;
using CampainScheduler.Domain.Models;
using CampainScheduler.Utils.CsvParsers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CampainScheduler.DAL.Seeders
{
    public class CustomerSeeder : IEntitySeeder<Customer>
    {
        private readonly ICsvParser<Customer> _csvParser;

        public CustomerSeeder(
            [FromKeyedServices("customerCsvParser")] ICsvParser<Customer> csvParser)
        {
            _csvParser = csvParser;
        }

        public List<Customer> GetEntities()
            => _csvParser.Parse("../InitialData/Customers/customers.csv");
    }
}
