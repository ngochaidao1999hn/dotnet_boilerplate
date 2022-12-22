using Application.Dtos.Identity;
using Application.Services.Interfaces;
using MediatR;

namespace Application.Commands.AuthenticationCommands
{
    public record RegisterCommand(RegisterDto register) : IRequest<bool>;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public RegisterCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.CreateUserAsync(request.register.userName, request.register.password);
        }
    }
}