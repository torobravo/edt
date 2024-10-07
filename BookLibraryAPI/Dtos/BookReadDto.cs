using BookLibraryAPI.Models;

namespace BookLibraryAPI.Dtos
{
    public class BookReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Isbn { get; set; }
        public int? PatronId { get; set; }
        public PatronReadDto? Patron { get; set; }
    }
}