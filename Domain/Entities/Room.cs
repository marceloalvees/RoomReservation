using Domain.Validation;

namespace Domain.Entities
{
    public class Room : Entity
    {
        public string Name { get; init; }
        public int Capacity { get; init; }
        public string Location { get; init; }

        public Room()
        {
        }

        public Room(string name, int capacity, string location)
        {
            Validate(name, capacity, location);
            Name = name;
            Capacity = capacity;
            Location = location;
        }

        public void Validate(string name, int capacity, string location)
        {
            DomainValidation.When(string.IsNullOrEmpty(name), "Name is required");
            DomainValidation.When(name.Length < 2, "Name must have at least 2 characters");
            DomainValidation.When(capacity < 1, "Capacity must be greater than 0");
            DomainValidation.When(string.IsNullOrEmpty(location), "Location is required");
        }
    }
}
