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

        public void CreateBook(Book book)
        {
            if (book == null)
                throw new ArgumentException(nameof(book));
            
            _context.Books.Add(book);
        }

        public void DeleteBook(Book book)
        {
            if (book == null)
                throw new ArgumentException(nameof(book));
            
            _context.Books.Remove(book);
        }

        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public bool SaveChanges()
        {
           return _context.SaveChanges() > 0;
        }

        public void UpdateBook(Book book)
        {
            // do nothing.  The DbContext will take care
        }
    }
}