using Application.Dto;
using AutoMapper;
using Domain.Abstractions;
using MediatR;

namespace Application.UserCases.Rooms.Queries.GetAll
{
    public class GetRoomsHandler : IRequestHandler<GetRoomsQuery, MessageResponseDto<List<GetRoomsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRoomsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MessageResponseDto<List<GetRoomsResponse>>>Handle(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var rooms = await _unitOfWork.RoomRepository.GetRoomsAsync(cancellationToken);
                var roomsResponse = _mapper.Map<List<GetRoomsResponse>>(rooms.ToList());
                return new MessageResponseDto<List<GetRoomsResponse>>(true,string.Empty, roomsResponse);
            }
            catch (Exception ex) 
            {
                return new MessageResponseDto<List<GetRoomsResponse>>(false, $"Error: {ex.Message}");
            }
        }
    }
}
