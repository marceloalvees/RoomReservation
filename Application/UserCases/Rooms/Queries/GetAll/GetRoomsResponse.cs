namespace Application.UserCases.Rooms.Queries.GetAll
{
    public class GetRoomsResponse
    {
        public int Id { get; set; }
        public required string Name { get; init; }
        public int Capacity { get; init; }
        public required string Location { get; init; }
    }
}
