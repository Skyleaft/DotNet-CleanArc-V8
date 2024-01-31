
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
using Microsoft.AspNetCore.Hosting;
using Application_Layer.ModuleCommand.TestGetUnmapped;
using System.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModuleController : ControllerBase
    {
        private readonly ISession _session;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ModuleController(ISession session, IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _session = session;
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Role>> Get([FromQuery] TestGetUnmappedCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
