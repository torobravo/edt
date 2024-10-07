using BookLibraryAPI.Models;

namespace BookLibraryAPI.Data
{
    public class MockPatronRepo : IPatronRepo
    {
        public Patron GetPatronById(int id)
        {
            return new Patron { Id = 1, Name = "Patron1", Email = "patron1@email.com" };
        }

        public IEnumerable<Patron> GetPatrons()
        {
            var patrons = new List<Patron> 
            {
                new() { Id = 1, Name = "MockPatron1", Email = "patron1@email.com" },
                new() { Id = 2, Name = "MockPatron1", Email = "patron1@email.com" }
            };

            return patrons;
        }
    }
}