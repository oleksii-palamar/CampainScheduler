namespace CampainScheduler.DAL.Seeders.Interfaces
{
    public interface IEntitySeeder<T>
    {
        List<T> GetEntities();
    }
}
