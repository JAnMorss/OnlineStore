using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Users.Errors
{
    public static class UserErrors
    {
        public static Error SellerProfileExists = new(
            "SellerProfile.Exists", 
            "Seller profile already exists.");

        public static Error NotFound = new(
            "User.Found", 
            "The user with the specified identifier was not found");

        public static Error CustomerProfileExists = new(
            "CustomerProfile.Exists", 
            "Customer profile already exists.");

        public static Error UserSameRole= new (
            "User.SameRole", 
            "The new role is the same as the current role.");

        public static Error PasswordEmpty = new(
            "Password.Empty",
            "Password cannot be empty.");
    }
}
