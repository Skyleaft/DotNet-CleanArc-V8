
using Web.Authentication;
using Web.Services;
using Web.Theme;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Data;
using System.Net.Http;
using DomainLayer.Models;

namespace Web.Shared
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }
        
        [Inject] private LayoutService LayoutService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] HttpClient httpClient { get; set; }


        [Parameter] public UserToken? currentAccountSession { get; set; }

        private MudThemeProvider _mudThemeProvider;
        private string idUser;
        private UserToken accountSession = new UserToken();

        

        protected override void OnInitialized()
        {
            LayoutService.SetBaseTheme(Themes.LandingPageTheme());
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            LayoutService.MajorUpdateOccured += LayoutServiceOnMajorUpdateOccured;
            base.OnInitialized();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await ApplyUserPreferences();
                await _mudThemeProvider.WatchSystemPreference(OnSystemPreferenceChanged);
                List<string> userSession = new List<string>();
                if (authenticationState.IsCompleted)
                {
                    var claim = authenticationState.Result.User.Claims;
                    foreach (var item in claim)
                    {
                        userSession.Add(item.Value);
                    }

                }
                if (userSession.Count > 0)
                {
                    idUser = userSession[2];
                }
                //if (idUser != null)
                //{
                //    accountSession = await httpClient.GetFromJsonAsync<UserAccount>("api/UserAccount/" + idUser);
                //}
                StateHasChanged();
            }
        }

        private async Task ApplyUserPreferences()
        {
            var defaultDarkMode = await _mudThemeProvider.GetSystemPreference();
            await LayoutService.ApplyUserPreferences(defaultDarkMode);
        }

        private async Task OnSystemPreferenceChanged(bool newValue)
        {
            await LayoutService.OnSystemPreferenceChanged(newValue);
        }

        public void Dispose()
        {
            LayoutService.MajorUpdateOccured -= LayoutServiceOnMajorUpdateOccured;
        }

        private void LayoutServiceOnMajorUpdateOccured(object sender, EventArgs e) => StateHasChanged();
    }
}
