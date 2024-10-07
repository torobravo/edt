using BookLibraryAPI.Models;

namespace BookLibraryAPI.Data
{
    public class InMemPatronRepo : IPatronRepo
    {
        private readonly LibraryContext _context;

        public InMemPatronRepo(LibraryContext context)
        { 
            _context = context;    
        }

        public Patron GetPatronById(int id)
        {
            return _context.Patrons.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Patron> GetPatrons()
        {
            return _context.Patrons.ToList();
        }
    }
}