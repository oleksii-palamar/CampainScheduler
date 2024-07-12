namespace CampainScheduler.Utils.Cron.Interfaces
{
    internal interface ICronConvertor
    {
        string TimeSpanToCron(TimeSpan value);
    }
}
