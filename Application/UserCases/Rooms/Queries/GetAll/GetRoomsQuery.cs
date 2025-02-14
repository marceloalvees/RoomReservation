using Application.Dto;
using MediatR;

namespace Application.UserCases.Rooms.Queries.GetAll
{
    public record class GetRoomsQuery() : IRequest<MessageResponseDto<List<GetRoomsResponse>>>;
}
