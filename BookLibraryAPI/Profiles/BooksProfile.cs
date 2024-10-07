using AutoMapper;
using BookLibraryAPI.Dtos;
using BookLibraryAPI.Models;

namespace BookLibraryAPI.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            // CreateMap<Book, BookReadDto>()
            // .ForMember(
            //         dest => dest.Id,
            //         src => src.MapFrom(x => x.Id))
            //     .ForMember(
            //         dest => dest.Title,
            //         src => src.MapFrom(x => x.Title))
            //     .ForMember(
            //         dest => dest.Author,
            //         src => src.MapFrom(x => $"Author: {x.Author}"))
            //     .ForMember(
            //         dest => dest.Year,
            //         src => src.MapFrom(x => x.Year))
            //     .ForMember(
            //         dest => dest.Isbn,
            //         src => src.MapFrom(x => x.Isbn));

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