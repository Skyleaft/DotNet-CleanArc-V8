using Application_Layer.UserAuth;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using DomainLayer.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ISession = DomainLayer.Interfaces.ISession;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ISession _session;
        private readonly IMediator _mediator;
        public UserController(ISession session, IMediator mediator)
        {
            _session = session;
            _mediator = mediator;
        }


        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        [TranslateResultToActionResult]
        [ExpectedFailures(ResultStatus.Invalid)]
        public async Task<Result<UserToken>> Authenticate([FromBody] UserAuthCommand request)
        {
            var token = await _mediator.Send(request);
            return token;

        }
    }
}
