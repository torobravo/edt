namespace BookLibraryAPI.Dtos
{
    public class BookCreateDto
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public int Year { get; set; }
        public required string Isbn { get; set; }       
    }
}