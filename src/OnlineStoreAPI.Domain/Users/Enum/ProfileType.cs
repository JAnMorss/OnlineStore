namespace OnlineStoreAPI.Domain.Users.Enum
{
    [Flags]
    public enum ProfileType
    {
        None     = 0,
        Customer = 1,
        Seller   = 2,
        Both     = Customer | Seller
    }
}
