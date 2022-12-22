using Application.Commands.AuthenticationCommands;
using Application.Dtos.Identity;
using Application.Queries.AuthenticationQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class UserController : BaseController
    {
        #region fields

        private IMediator _mediator;

        #endregion fields

        #region ctor

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion ctor

        #region methods
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login) {
            var token = await _mediator.Send(new LoginQuery(login));
            if (token.accessToken == null)
                return BadRequest("Username/Password not correct");
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            var res = await _mediator.Send(new RegisterCommand(register));
            if (!res)
                return BadRequest();
            return Ok();
        }
        #endregion methods
    }
}