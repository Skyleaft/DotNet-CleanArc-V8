using DomainLayer.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace Web.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<UserToken>("UserToken");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                new Claim(ClaimTypes.Name,userSession.Id.ToString()),
                new Claim(ClaimTypes.Role,userSession.User.Role.Name),
                new Claim(ClaimTypes.Authentication,userSession.Token),
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserToken userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserToken", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                new Claim(ClaimTypes.Name,userSession.Id.ToString()),
                new Claim(ClaimTypes.Role,userSession.User.Role.Name),
                new Claim(ClaimTypes.Authentication,userSession.Token),
                }));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserToken");
                
                claimsPrincipal = _anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
