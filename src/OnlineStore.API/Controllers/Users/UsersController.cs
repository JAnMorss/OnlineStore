using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Users.Commands.RegisterUser;

namespace OnlineStore.API.Controllers.Users
{
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegiserUserRequest request, 
            CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                request.UserName,
                request.FirstName, 
                request.LastName,
                request.Email,
                request.PhoneNumber,
                request.Password);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
