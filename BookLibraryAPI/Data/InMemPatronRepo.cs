using BookLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Data
{
    public class InMemPatronRepo : IPatronRepo
    {
        private readonly LibraryContext _context;

        public InMemPatronRepo(LibraryContext context)
        { 
            _context = context;    
        }

        public void CreatePatron(Patron patron)
        {
            if (patron == null)
                throw new ArgumentException(nameof(patron));
            
            _context.Patrons.Add(patron);
        }

        public Patron GetPatronById(int id)
        {
            return _context.Patrons.Include(p => p.BorrowedBooks).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Patron> GetPatrons()
        {
            return _context.Patrons.Include(p => p.BorrowedBooks).ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdatePatron(Patron patron)
        {
            // do nothing. DbContext will take care
        }
    }
}