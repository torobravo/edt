using BookLibraryAPI.Data;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepo _repo;

        public BooksController(IBookRepo repository)
        {
            _repo = repository;
        }

        // GET: api/books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            var books = _repo.GetBooks();

            return Ok(books);
        }

        // GET: api/books/1
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _repo.GetBookById(id);

            if (book == null)
                return NotFound();

            return book;
        }

    }
}