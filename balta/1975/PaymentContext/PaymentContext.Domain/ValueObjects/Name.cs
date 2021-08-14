using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications((new Contract())
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "Min 3 chars")
                .HasMinLen(LastName, 3, "Name.LastName", "Min 3 chars")
                .HasMaxLen(FirstName, 40, "Name.FirstName", "Max 40 chars")
                .HasMaxLen(LastName, 40, "Name.LastName", "Max 40 chars"));

            if (string.IsNullOrEmpty(firstName) )
                AddNotification("Name.FirstName", "Invalid name");
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}