using Application_Layer.UserCommand.UserAuth;
using Application_Layer.UserCommand.UserCreate;
using Application_Layer.UserCommand.UserDelete;
using Application_Layer.UserCommand.UserGet;
using Application_Layer.UserCommand.UserUpdate;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using DomainLayer.Common.Responses;
using DomainLayer.Extensions;
using DomainLayer.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Net;
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

        [ProducesResponseType(typeof(PaginatedList<User>), StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PaginatedList<User>>> Get([FromQuery] UserGetCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        [AllowAnonymous]
        [TranslateResultToActionResult]
        [ExpectedFailures(ResultStatus.Invalid)]
        public async Task<Result<User>> Create([FromForm] UserCreateCommand request)
        {
            var result = await _mediator.Send(request);
            return result;
        }

        [AllowAnonymous]
        [HttpPut("{Id}")]
        [TranslateResultToActionResult]
        [ExpectedFailures(ResultStatus.Invalid, ResultStatus.NotFound)]
        public async Task<Result<User>> Update([FromForm] UserUpdateCommand request)
        {
            var result = await _mediator.Send(request);
            return result;
        }

        [AllowAnonymous]
        [HttpDelete("{Id}")]
        [TranslateResultToActionResult]
        [ExpectedFailures(ResultStatus.Invalid, ResultStatus.NotFound)]
        public async Task<Result> Delete(UserDeleteCommand request)
        {
            var result = await _mediator.Send(request);
            return result;
        }


        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        [TranslateResultToActionResult]
        [ExpectedFailures(ResultStatus.Invalid)]
        public async Task<Result<UserToken>> Authenticate([FromBody] UserAuthCommand request)
        {
            IPAddress remoteIpAddress = new GetIP().GetRemoteHostIpAddressUsingXRealIp(HttpContext);
            if(remoteIpAddress== null)
            {
                remoteIpAddress = new GetIP().GetRemoteHostIpAddressUsingRemoteIpAddress(HttpContext);
            }
            request.setIpAdr(remoteIpAddress.ToString());
            var token = await _mediator.Send(request);
            return token;

        }
    }
}
