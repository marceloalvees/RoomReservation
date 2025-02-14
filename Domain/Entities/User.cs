using Domain.Validation;

namespace Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Email { get; init; } //Esse campo é buscar as reservas, por isso foi colocado como init
        public string PhoneNumber { get; private set; }

        public User()
        {
        }

        public User(string name, string email, string phoneNumber)
        {
            Validate(name, email, phoneNumber);
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void Update(string name, string phoneNumber)
        {
            Validate(name, Email, phoneNumber);
            Name = name;
            PhoneNumber = phoneNumber;
        }

        private void Validate(string name, string email, string phoneNumber) {
            DomainValidation.When(string.IsNullOrEmpty(name), "Name is required");
            DomainValidation.When(name.Length < 2, "Name must have at least 2 characters");
            DomainValidation.When(string.IsNullOrEmpty(email), "Email is required");
            DomainValidation.When(email.Length < 6, "Invalid Email, too short, minimum 6 characters");
            DomainValidation.When(!email.Contains("@"), "Invalid Email, must contain @");
            DomainValidation.When(string.IsNullOrEmpty(phoneNumber), "Phone number is required");
        }
    }
}
