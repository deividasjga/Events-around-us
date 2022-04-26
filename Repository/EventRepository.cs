using Microsoft.EntityFrameworkCore;
using RenginiaiWebApp.Data;
using RenginiaiWebApp.Interfaces;
using RenginiaiWebApp.Models;

namespace RenginiaiWebApp.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Event events)   // event
        {
            _context.Add(events);  // event
            return Save();
        }

        public bool Delete(Event events)
        {
            _context.Remove(events);
            return Save();
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetAllEventsByCity(string city)
        {
            return await _context.Events.Include(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Event> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Events.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Event events)
        {
            _context.Update(events);
            return Save();
        }
    }
}
