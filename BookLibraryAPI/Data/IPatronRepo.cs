using BookLibraryAPI.Models;

namespace BookLibraryAPI.Data
{
    public interface IPatronRepo
    {
        IEnumerable<Patron> GetPatrons();

        Patron GetPatronById(int id);
    }
}