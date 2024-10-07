namespace BookLibraryAPI.Dtos
{
    public class PatronCreateDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }
}