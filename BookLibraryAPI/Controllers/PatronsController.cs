using AutoMapper;
using BookLibraryAPI.Data;
using BookLibraryAPI.Dtos;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatronsController : ControllerBase
    {
        private readonly IPatronRepo _repo;
        private readonly IMapper _mapper;

        public PatronsController(IPatronRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        // GET: api/patrons
        [HttpGet]
        public ActionResult<IEnumerable<PatronReadDto>> GetPatrons()
        {
            // Database retrieval
            var patrons = _repo.GetPatrons();

            // 200-OK
            return Ok(_mapper.Map<IEnumerable<PatronReadDto>>(patrons));
        }

        // GET: api/patrons/1
        [HttpGet("{id}", Name = "GetPatronById")]
        public ActionResult<PatronReadDto> GetPatronById(int id)
        {
            // Database lookup
            var patron = _repo.GetPatronById(id);

            // 404-Not Found
            if (patron == null)
                return NotFound();

            // Convert to dto
            var patronReadDto = _mapper.Map<PatronReadDto>(patron);

            //200-OK
            return Ok(patronReadDto);
        }


        // POST: api/patrons
        [HttpPost]
        public ActionResult<PatronReadDto> CreatePatron([FromBody] PatronCreateDto patronCreateDto)
        {
            var patronModel = _mapper.Map<Patron>(patronCreateDto);

            // Create into database
            _repo.CreatePatron(patronModel);
            _repo.SaveChanges();

            // Convert to dto
            var patronReadDto = _mapper.Map<PatronReadDto>(patronModel);

            // 201-Created
            return CreatedAtRoute(nameof(GetPatronById), new { Id = patronReadDto.Id }, patronReadDto);
        }

    }
}