using BookLibraryAPI.Models;

namespace BookLibraryAPI.Data
{
    public interface IPatronRepo
    {
        bool SaveChanges();
        IEnumerable<Patron> GetPatrons();
        Patron GetPatronById(int id);
        void CreatePatron(Patron patron);
        void UpdatePatron(Patron patron);
    }
}