namespace BookLibraryAPI.Models
{
    public class Patron
    {
        public int Id {get; set;}
        public required string Name {get; set;}
        public required string Email {get; set;}

        public List<Book> BorrowedBooks {get; set; } = [];
    }
}