using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string Street { get; }

        public string City { get; }

        public string State { get; }

        public string ZipCode { get; }

        public string Country { get; }

        private Address(
            string street,
            string city,
            string state,
            string zipCode,
            string country)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }

        public static Result<Address> Create(
            string street,
            string city,
            string state,
            string zipCode,
            string country)
        {
            if (string.IsNullOrWhiteSpace(street) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(state) ||
                string.IsNullOrWhiteSpace(zipCode) ||
                string.IsNullOrWhiteSpace(country))
            {
                return Result.Failure<Address>(new Error(
                    "Address.Invalid",
                    "All address fields must be provided and non-empty."));
            }

            return Result.Success(new Address(
                street.Trim(),
                city.Trim(),
                state.Trim(),
                zipCode.Trim(),
                country.Trim()));
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return ZipCode;
            yield return Country;
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {Street}, {ZipCode}, {Country}";
        }
    }
}
