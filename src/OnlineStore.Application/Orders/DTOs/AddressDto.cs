namespace OnlineStore.Application.Orders.DTOs
{
    public sealed class AddressDto
    {
        public string Street { get; set; } = default!;

        public string City { get; set; } = default!;

        public string Barangay { get; set; } = default!;

        public string ZipCode { get; set; } = default!;

        public string Country { get; set; } = default!;
    }
}
