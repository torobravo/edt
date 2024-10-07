using BookLibraryAPI.Models;

namespace BookLibraryAPI.Data
{
    public interface IBookRepo {
        bool SaveChanges();
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}