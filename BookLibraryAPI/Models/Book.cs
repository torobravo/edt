using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPI.Models
{
    public class Book
    {
        [Key]        
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public int Year { get; set; }
        public required string Isbn { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ModifiedBy {get; set;}

        public int? PatronId { get; set; }
        public Patron? Patron { get; set; }
    }
}