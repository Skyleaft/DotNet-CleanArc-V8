
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using ISession = DomainLayer.Interfaces.ISession;

namespace DomainLayer.Models.Auth;

public class Session : ISession
{
    public int UserId { get; private init; }

    public DateTime Now => DateTime.Now;

    public Session(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        var nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);

        if(nameIdentifier != null)
        {
            UserId = int.Parse(nameIdentifier.Value);
        }
    }

}