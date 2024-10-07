using BookLibraryAPI.Models;

namespace BookLibraryAPI.Data
{
    public class MockBookRepo : IBookRepo
    {
        public Book GetBookById(int id)
        {
            return new Book { Id = 1, Title = "MockBookName1", Author = "MockAuthor1", Year = 1990, Isbn = "12345X"};
        }

        public IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>
            {
                new() { Id = 1, Title = "MockBookName1", Author = "MockAuthor1", Year = 1990, Isbn = "12345X"},
                new() { Id = 2, Title = "MockBookName2", Author = "MockAuthor2", Year = 1991, Isbn = "12345Y"},
                new() { Id = 3, Title = "MockBookName2", Author = "MockAuthor3", Year = 1992, Isbn = "12345Z"}
            };

            return books;
        }
    }
}