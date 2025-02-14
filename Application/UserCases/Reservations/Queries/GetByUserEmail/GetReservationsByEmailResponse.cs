using Domain.Enuns;

namespace Application.UserCases.Reservations.Queries.GetByUserEmail
{
    public class GetReservationsByEmailResponse
    {
        public int Id { get; set; }
        public required string RoomName { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCancelation { get; set; }
        public EReservationStatus Status { get; set; }
        public int Capacity { get; set; }
    }
}
