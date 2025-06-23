using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Shared
{
    public sealed class Address : ValueObject
    {


        public string Street { get; }

        public string City { get; }

        public string Barangay { get; }

        public string ZipCode { get; }

        public string Country { get; }

        public Address(
            string street,
            string city,
            string barangay,
            string zipCode,
            string country)
        {
            Street = street;
            City = city;
            Barangay = barangay;
            ZipCode = zipCode;
            Country = country;
        }

        public static Result<Address> Create(
            string street,
            string city,
            string barangay,
            string zipCode,
            string country)
        {
            if (string.IsNullOrWhiteSpace(street) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(barangay) ||
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
                barangay.Trim(),
                zipCode.Trim(),
                country.Trim()));
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return Barangay;
            yield return ZipCode;
            yield return Country;
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {Barangay}, {ZipCode}, {Country}";
        }
    }
}
