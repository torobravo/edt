using BookLibraryAPI.Models;

namespace BookLibraryAPI.Data
{
    public class InMemBookRepo : IBookRepo
    {
        private readonly LibraryContext _context;

        public InMemBookRepo(LibraryContext context)
        {
            _context = context;
        }
        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }
    }
}