using OnlineStore.Application.Abstraction.Authentication;
using OnlineStoreAPI.Domain.Users.Entities;
using OnlineStoreAPI.Domain.Users.Interface;
using OnlineStoreAPI.Domain.Users.ValueObjects;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.Application.Command;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Users.Commands.RegisterUser
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(
            IAuthenticationService authenticationService, 
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            RegisterUserCommand request, 
            CancellationToken cancellationToken)
        {
            var userResult = User.Create(
                new UserName(request.UserName),
                new FirstName(request.FirstName),
                new LastName(request.LastName),
                new EmailVO(request.Email),
                new PhoneNumber(request.PhoneNumber)
            );

            var user = userResult.Value;

            var identityId = await _authenticationService.RegisterAsync(user, request.Password, cancellationToken);

            user.SetIdentityId(identityId);

            _userRepository.Add(user);

            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }
    
}
