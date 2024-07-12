using CampainScheduler.Utils.Cron.Interfaces;

namespace CampainScheduler.Utils.Cron
{
    internal class CronConvertor : ICronConvertor
    {
        public string TimeSpanToCron(TimeSpan value)
        {
            var cron = $"{value.Minutes} {value.Hours} * * *";

            return cron;
        }
    }
}
