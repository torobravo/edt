using BookLibraryAPI.Models;

namespace BookLibraryAPI.Data
{
    public class MockPatronRepo : IPatronRepo
    {
        public void CreatePatron(Patron patron)
        {
            throw new NotImplementedException();
        }

        public Patron GetPatronById(int id)
        {
            return new Patron { Id = 1, FirstName = "MockFname1", LastName = "MockFname1", Email = "patron1@email.com" };
        }

        public IEnumerable<Patron> GetPatrons()
        {
            var patrons = new List<Patron> 
            {
                new() { Id = 1, FirstName = "MockFname1", LastName = "MockFname1", Email = "patron1@email.com" },
                new() { Id = 2, FirstName = "MockFname2", LastName = "MockLname2", Email = "patron1@email.com" }
            };

            return patrons;
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}