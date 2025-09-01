namespace OnlineStore.API.Controllers.Users
{
    public sealed record RegiserUserRequest(
        string UserName,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string Password);
}
