using AutoMapper;
using BookLibraryAPI.Data;
using BookLibraryAPI.Dtos;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepo _bookRepo;
        private readonly IPatronRepo _patronRepo;
        private readonly IMapper _mapper;

        public BooksController(IBookRepo repository, IPatronRepo patronRepo, IMapper mapper)
        {
            _bookRepo = repository;
            _patronRepo = patronRepo;
            _mapper = mapper;
        }

        // GET: api/books
        [HttpGet]
        public ActionResult<IEnumerable<BookReadDto>> GetBooks()
        {
            // Database lookup
            var books = _bookRepo.GetBooks();

            // return 200-OK
            return Ok(_mapper.Map<IEnumerable<BookReadDto>>(books));
        }

        // GET: api/books/1
        [HttpGet("{id}", Name = "GetBookById")]
        public ActionResult<BookReadDto> GetBookById(int id)
        {
            // Database lookup
            var book = _bookRepo.GetBookById(id);

            // 404-Not Found
            if (book == null)
                return NotFound();

            // Convert to dto
            var bookReadDto = _mapper.Map<BookReadDto>(book);

            // return 200-OK
            return Ok(bookReadDto);
        }

        // POST: api/books
        [HttpPost]
        public ActionResult<BookReadDto> CreateBook([FromBody] BookCreateDto bookCreateDto)
        {
            var bookModel = _mapper.Map<Book>(bookCreateDto);

            // create into database
            _bookRepo.CreateBook(bookModel);
            _bookRepo.SaveChanges();

            // convert to dto
            var bookReadDto = _mapper.Map<BookReadDto>(bookModel);

            // return 201-Created
            return CreatedAtRoute(nameof(GetBookById), new { Id = bookReadDto.Id }, bookReadDto);
        }

        //PUT api/books/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookUpdateDto bookUpdateDto)
        {
            // Database lookup
            var bookModelFromRepo = _bookRepo.GetBookById(id);

            // 404-Not Found
            if (bookModelFromRepo == null)
                return NotFound();

            // map objects
            _mapper.Map(bookUpdateDto, bookModelFromRepo);

            // Update database
            _bookRepo.UpdateBook(bookModelFromRepo);
            _bookRepo.SaveChanges();

            // 204-No Content
            return NoContent();
        }

        //PATCH api/books/{id}
        [HttpPatch("{id}")]
        public IActionResult PartialBookUpdate(int id, JsonPatchDocument<BookUpdateDto> patchBook)
        {
            // Database lookup
            var bookModelFromRepo = _bookRepo.GetBookById(id);

            // 404-Not Found
            if (bookModelFromRepo == null)
                return NotFound();

            var bookToPatch = _mapper.Map<BookUpdateDto>(bookModelFromRepo);
            patchBook.ApplyTo(bookToPatch, ModelState);

            if (!TryValidateModel(bookToPatch))
                return ValidationProblem(ModelState);

            // map objects
            _mapper.Map(bookToPatch, bookModelFromRepo);

            // Update database
            _bookRepo.UpdateBook(bookModelFromRepo);
            _bookRepo.SaveChanges();

            // 204-No Content
            return NoContent();
        }

        //DELETE api/books/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            // Database lookup
            var bookModelFromRepo = _bookRepo.GetBookById(id);

            // 404-Not Found
            if (bookModelFromRepo == null)
                return NotFound();
            
            // 400-BadRequest Book is checked out.
            if (bookModelFromRepo.PatronId != null)
                return BadRequest("Cannot delete book. It is checked out.");

            // Update database
            _bookRepo.DeleteBook(bookModelFromRepo);
            _bookRepo.SaveChanges();

            // 204-No Content
            return NoContent();
        }

        // PUT: api/books/1/checkout/1
        [HttpPut("{patronId}/checkout/{bookId}")]
        public IActionResult CheckoutBook(int patronId, int bookId )
        {
            // Database lookup
            var bookModel = _bookRepo.GetBookById(bookId);
            var patronModel = _patronRepo.GetPatronById(patronId);

            // 404-Not Found
            if (bookModel == null || patronModel == null)
                return NotFound();

            // 400-BadRequest already checked out.
            if (bookModel.PatronId != null)
                return BadRequest("The book is already checked out.");

            // Update model
            bookModel.PatronId = patronId;
            patronModel.BorrowedBooks.Add(bookModel);

            // Update database
            _bookRepo.SaveChanges();
            _patronRepo.SaveChanges();

            // 204-No Content
            return NoContent();
        }

        // PUT: api/books/checkin/1
        [HttpPut("checkin/{bookId}")]
        public IActionResult CheckinBook(int bookId)
        {
            // Database lookup
            var bookModel = _bookRepo.GetBookById(bookId);

            // 404-Not Found
            if (bookModel == null)
                return NotFound();

            // 400-Bad Request: Not checked out
            if (bookModel.PatronId == null)
                return BadRequest("The book is not checked out.");

            // Update model
            var patron = bookModel.Patron;
            patron.BorrowedBooks.Remove(bookModel);
            bookModel.PatronId = null;

            // Update database
            _bookRepo.SaveChanges();
            _patronRepo.SaveChanges();

            // 204-No Content
            return NoContent();
        }
    }
}