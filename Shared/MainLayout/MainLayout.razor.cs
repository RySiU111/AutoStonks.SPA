using System;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services.AuthService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Newtonsoft.Json.Linq;

namespace AutoStonks.SPA.Shared.MainLayout
{
    public partial class MainLayout : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAuthService AuthService { get; set; }

        private User _user;
        private EventHandler<LocationChangedEventArgs> checkUser;


        protected override async Task OnInitializedAsync()
        {
            _user = await AuthService.GetUser();
            checkUser = async (o, args) => await CheckUser();
            NavigationManager.LocationChanged += checkUser;
        }

        private void NavigateToLogin()
        {
            NavigationManager.NavigateTo("login");
        }

        private void NavigateToRegister()
        {
            NavigationManager.NavigateTo("register");
        }

        private async Task CheckUser()
        {
            if(_user == null)
            {
                _user = await AuthService.GetUser();
                if(_user != null)
                    StateHasChanged();
            }
        }

        private async Task Logout()
        {
            await AuthService.Logout();
            _user = null;

            NavigateToLogin();
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= checkUser;
        }
    }
}