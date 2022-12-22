using Application.Dtos.Identity;
using Application.Models;
using Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.AuthenticationQueries
{
   public record LoginQuery(LoginDto login) : IRequest<AuthenticationResponse>;
   public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResponse>
   {
        private readonly IIdentityService _identityService;
        public LoginQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<AuthenticationResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var token = await _identityService.AuthorizeAsync(request.login.userName, request.login.password);
            return token;         
        }
   }
}
