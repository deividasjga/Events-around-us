using RenginiaiWebApp.Models;

namespace RenginiaiWebApp.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAll();
        Task<Event> GetByIdAsync(int id);
        Task<Event> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Event>> GetAllEventsByCity(string city);
        bool Add(Event events);  //event
        bool Update(Event events);   //event
        bool Delete(Event events);   //event
        bool Save();
    }
}