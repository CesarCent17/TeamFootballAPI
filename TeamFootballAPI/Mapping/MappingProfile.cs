using AutoMapper;
using TeamFootballAPI.Models;
using TeamFootballAPI.Models.Dto;

namespace TeamFootballAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TeamCreateDto, Team>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        }
    }
}
