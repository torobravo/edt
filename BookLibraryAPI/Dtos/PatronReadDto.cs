using BookLibraryAPI.Models;

namespace BookLibraryAPI.Dtos
{
    public class PatronReadDto
    {
        public int Id {get; set;}
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<BookReadDto> BorrowedBooks { get; set; } = [];
    }
}