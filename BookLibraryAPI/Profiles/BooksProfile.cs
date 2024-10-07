using AutoMapper;
using BookLibraryAPI.Dtos;
using BookLibraryAPI.Models;

namespace BookLibraryAPI.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Book, BookReadDto>();

            CreateMap<BookCreateDto, Book>()
                .ForMember(
                    dest => dest.CreatedAt,
                    src => src.MapFrom(x => DateTime.Now)
                );
            
            CreateMap<BookUpdateDto, Book>();

            CreateMap<Book, BookUpdateDto>();
            
        }
    }
}