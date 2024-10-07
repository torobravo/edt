using AutoMapper;
using BookLibraryAPI.Dtos;
using BookLibraryAPI.Models;

namespace BookLibraryAPI.Profiles
{
    public class PatronsProfile : Profile
    {
        public PatronsProfile()
        {
            // Converts Patron to PatronReadDto
            CreateMap<Patron, PatronReadDto>()
                .ForMember(
                    dest => dest.FullName,
                    src => src.MapFrom(x => x.LastName + ", " + x.FirstName)
                );

            // Converts PatronCreateDto to Patron
            CreateMap<PatronCreateDto, Patron>();
        }
    }
}