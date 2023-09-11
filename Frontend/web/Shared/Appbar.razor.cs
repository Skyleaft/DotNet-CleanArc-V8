
using Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using DomainLayer.Models;
using Web.Authentication;
using System.Net.Http;
using System.Security.Principal;
using System.Security.Claims;

namespace Web.Shared
{
    public partial class Appbar
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }
        [Inject] private LayoutService LayoutService { get; set; }
        [Inject] NavigationManager navManager { get; set; }
        [Inject] AuthenticationStateProvider authStateProvicder { get; set; }
        [Parameter] public UserToken? currentAccountSession { get; set; }
        [Inject] HttpClient httpClient { get; set; }
        private async Task Logout()
        {
            var user = authenticationState.Result.User;
            var claims = user.Claims;
            if (user.Identity.IsAuthenticated)
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvicder;
                var response = await httpClient.PostAsJsonAsync(@"api/User/logout", claims.FirstOrDefault(x=>x.ValueType.Equals("Name"))); ;
                await customAuthStateProvider.UpdateAuthenticationState(null);
            }
            



            navManager.NavigateTo("/");

        }
    }
}
