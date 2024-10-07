using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPI.Models
{
    public class Patron
    {
        [Key]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public List<Book> BorrowedBooks { get; set; } = [];
    }
}