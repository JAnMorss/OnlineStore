using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class Role : ValueObject
    {
        public string Value { get; }

        private Role(string value)
        {
            Value = value;
        }

        public static readonly Role Admin = new("Admin");
        public static readonly Role User = new("User");
        public static readonly Role Guest = new("Guest");

        public static IEnumerable<Role> List() => new[] { 
                Admin,
                User, 
                Guest 
            };

        public static Result<Role> FromString(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return Result.Failure<Role>(new Error(
                    "Role.Empty",
                    "Role cannot be null or whitespace."));
            }

            var normalized = role.Trim();

            var match = List().FirstOrDefault(r =>
                string.Equals(r.Value, normalized, StringComparison.OrdinalIgnoreCase));

            return match is not null
                ? Result.Success(match)
                : Result.Failure<Role>(new Error(
                    "Role.Invalid",
                    $"Role '{role}' is not recognized. Valid roles are: {
                        string.Join(", ", 
                        List().Select(r => r.Value))}."));
        }

        public bool Is(Role other)  
            => other is not null &&
               string.Equals(
                   Value, 
                   other.Value, 
                   StringComparison.OrdinalIgnoreCase);


        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
