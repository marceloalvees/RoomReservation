using Application.UserCases.Rooms.Queries.GetAll;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Room, GetRoomsResponse>();
        }
    }
}
