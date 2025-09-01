namespace OnlineStore.Application.Orders.Responses
{
    public sealed class AddressResponse
    {
        public string Street { get; set; } = default!;

        public string City { get; set; } = default!;

        public string Barangay { get; set; } = default!;

        public string ZipCode { get; set; } = default!;

        public string Country { get; set; } = default!;
    }
}
