using BookLibraryAPI.Models;

namespace BookLibraryAPI.Data
{
    public interface IBookRepo {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
    }
}