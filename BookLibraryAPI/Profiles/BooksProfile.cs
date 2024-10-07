using AutoMapper;
using BookLibraryAPI.Dtos;
using BookLibraryAPI.Models;

namespace BookLibraryAPI.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            // Converts Book to BookReadDto
            CreateMap<Book, BookReadDto>();

            // Converts BookCreateDto to Book
            CreateMap<BookCreateDto, Book>()
                .ForMember(
                    dest => dest.CreatedAt,
                    src => src.MapFrom(x => DateTime.Now)
                );

            // Converts BookUpdateDto to Book
            CreateMap<BookUpdateDto, Book>();

            // Converts Book to BookUpdateDto
            CreateMap<Book, BookUpdateDto>();

        }
    }
}