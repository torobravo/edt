using BookLibraryAPI.Data;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatronsController : ControllerBase
    {
        private readonly IPatronRepo _repo;

        public PatronsController(IPatronRepo repository)
        {
            _repo = repository;
        }

        // GET: api/patrons
        [HttpGet]
        public ActionResult<IEnumerable<Patron>> GetPatrons()
        {
            var patrons = _repo.GetPatrons();

            return Ok(patrons);
        }

        // GET: api/patrons/1
        [HttpGet("{id}")]
        public ActionResult<Patron> GetPatron(int id)
        {
            var patron = _repo.GetPatronById(id);

            if (patron == null)
                return NotFound();

            return patron;
        }
    }
}