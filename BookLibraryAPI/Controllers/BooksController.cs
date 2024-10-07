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
        private readonly IBookRepo _repo;
        private readonly IMapper _mapper;

        public BooksController(IBookRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        // GET: api/books
        [HttpGet]
        public ActionResult<IEnumerable<BookReadDto>> GetBooks()
        {
            // Database lookup
            var books = _repo.GetBooks();

            // return 200-OK
            return Ok(_mapper.Map<IEnumerable<BookReadDto>>(books));
        }

        // GET: api/books/1
        [HttpGet("{id}", Name = "GetBookById")]
        public ActionResult<BookReadDto> GetBookById(int id)
        {
            // Database lookup
            var book = _repo.GetBookById(id);

            // 404-Not Found
            if (book == null)
                return NotFound();

            // Conver to dto
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
            _repo.CreateBook(bookModel);
            _repo.SaveChanges();

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
            var bookModelFromRepo = _repo.GetBookById(id);

            // 404-Not Found
            if (bookModelFromRepo == null)
                return NotFound();
            
            // map objects
            _mapper.Map(bookUpdateDto, bookModelFromRepo);
            
            // Update database
            _repo.UpdateBook(bookModelFromRepo);
            _repo.SaveChanges();

            // 204-No Content
            return NoContent();
        }

        //PATCH api/books/{id}
        [HttpPatch("{id}")]
        public IActionResult PartialBookUpdate(int id, JsonPatchDocument<BookUpdateDto> patchBook) 
        {
            // Database lookup
            var bookModelFromRepo = _repo.GetBookById(id);

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
            _repo.UpdateBook(bookModelFromRepo);
            _repo.SaveChanges();

            // 204-No Content
            return NoContent();
        }

        //DELETE api/books/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            // Database lookup
            var bookModelFromRepo = _repo.GetBookById(id);

            // 404-Not Found
            if (bookModelFromRepo == null)
                return NotFound();

            // Update database
            _repo.DeleteBook(bookModelFromRepo);
            _repo.SaveChanges();

            // 204-No Content
            return NoContent();
        }

    }
}