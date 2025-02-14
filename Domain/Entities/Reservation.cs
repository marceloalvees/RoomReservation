using System.ComponentModel.DataAnnotations;
using Domain.Enuns;
using Domain.Validation;

namespace Domain.Entities
{
    public class Reservation : Entity
    {
        public int RoomId { get; init; }
        public int UserId { get; init; }
        public DateTime Date { get; private set; }
        public EReservationStatus Status { get; private set; }
        public DateTime? DateCancelation { get; private set; }

        public Reservation()
        {
        }

        public Reservation(Room room, User user, DateTime date)
        {
            Validate(room, user, date);
            RoomId = room.Id;
            UserId = user.Id;
            Date = date;
            Status = EReservationStatus.Confirmed;
        }

        public void Cancel()
        {
            Status = EReservationStatus.Canceled;
            DateCancelation = DateTime.Now;
        }

        public void Update(DateTime date)
        {
            ValidateDate(date);
            Date = date;
        }

        private void ValidateDate(DateTime date)
        {
            DomainValidation.When(date < DateTime.Now, "Date must be greater than or equal to the current date");
        }

        private void Validate(Room room, User user, DateTime date)
        {
            var msgRoom = "Room is required";
            var msgUser = "User is required";

            DomainValidation.When(room == null, msgRoom);
            DomainValidation.When(room?.Id < 1, msgRoom);
            DomainValidation.When(user == null, msgUser);
            DomainValidation.When(user?.Id < 1, msgUser);
            DomainValidation.When(date < DateTime.Now, "Date must be greater than or equal to the current date");
        }
    }
}
