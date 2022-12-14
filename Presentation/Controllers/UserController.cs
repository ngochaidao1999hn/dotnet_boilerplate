using Application.Dtos.Identity;
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

        #endregion methods
    }
}