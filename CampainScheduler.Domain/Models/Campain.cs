namespace CampainScheduler.Domain.Models
{
    public class Campain
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string CustomersCondition { get; set; }
        public TimeSpan ScheduledTimeUtc { get; set; }
        public int Priority { get; set; }

        public Template Template { get; set; }

        public bool Any()
        {
            throw new NotImplementedException();
        }
    }
}
