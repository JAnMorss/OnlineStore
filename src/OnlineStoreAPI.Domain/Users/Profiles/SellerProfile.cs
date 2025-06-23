using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Domain.Users.ValueObjects;

namespace OnlineStoreAPI.Domain.Users.Profiles
{
    public sealed class SellerProfile
    {
        public ShopName ShopName { get; private set; }

        public StoreDescription StoreDescription { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public Address Address { get; private set; }

        public SellerProfile(
            ShopName shopName,
            StoreDescription storeDescription,
            PhoneNumber phoneNumber,
            Address address
            )
        {
            ShopName = shopName;
            StoreDescription = storeDescription;
            PhoneNumber = phoneNumber;
            Address = address;
        }

    }
}
