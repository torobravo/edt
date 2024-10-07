namespace BookLibraryAPI.Dtos
{
    public class BookUpdateDto : BookCreateDto
    {
        public required string ModifiedBy { get; set; }
    }
}