using AutoMapper;
using BookLibraryAPI.Dtos;
using BookLibraryAPI.Models;

namespace BookLibraryAPI.Profiles
{
    public class PatronsProfile : Profile
    {
        public PatronsProfile()
        {
            CreateMap<Patron, PatronReadDto>()
                .ForMember(
                    dest => dest.FullName,
                    src => src.MapFrom(x => x.LastName + ", " + x.FirstName)
                );

            CreateMap<PatronCreateDto, Patron>();
        }
    }
}