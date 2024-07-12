using CampainScheduler.Domain.Enums;

namespace CampainScheduler.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public CustomerGender Gender { get; set; }
        public string City { get; set; }
        public decimal Deposit { get; set; }
        public bool IsNewCusomer { get; set; }
    }
}
