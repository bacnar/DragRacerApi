using AutoMapper;
using DragRacerApi.Entities;
using DragRacerApi.Models;

namespace DragRacerApi.Profiles
{
    public class RacerProfile : Profile
    {
        public RacerProfile()
        {
            CreateMap<RacerDto, Race>().ReverseMap();

            CreateMap<RaceResponseDto, Race>().ReverseMap();

            CreateMap<UserResponseDto, User>().ReverseMap();

            CreateMap<SessionResponseDto, Session>().ReverseMap();
        }
    }
}
