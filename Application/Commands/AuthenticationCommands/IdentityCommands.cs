using Application.Dtos.Identity;
using Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.AuthenticationCommands
{
    public record RegisterCommand(RegisterDto register) : IRequest<IdentityResult>;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IdentityResult>
    {
        private readonly IIdentityService _identityService;

        public RegisterCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<IdentityResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.CreateUserAsync(request.register.userName, request.register.password);           
        }
    }
}