using System;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services.AuthService;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace AutoStonks.SPA.Pages.Auth.Login
{
    public partial class Login
    {
        [Inject]
        public IAuthService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManger { get; set; }

        [Parameter]
        public string RedirectUrl { get; set; }

        private User _userCredentials = new User();
        private string _message;
        private Snackbar _snackbar;

        private async Task HandleValidSubmit()
        {
            var response = await AuthService.Login(_userCredentials);

            if(!response.Success)
            {
                _userCredentials = new User();
                SetSnackbar(response.Message, SnackbarColor.Danger);
            }
            else
            {
                if(!String.IsNullOrEmpty(RedirectUrl))
                    NavigationManger.NavigateTo(RedirectUrl.Replace('_','/'));
                else
                    NavigationManger.NavigateTo("/");
            } 
        }

        private void SetSnackbar(string message, SnackbarColor color)
        {
            System.Console.WriteLine(message);
            _message = message;
            _snackbar.Location = SnackbarLocation.Right;
            _snackbar.Multiline = true;
            _snackbar.Color = color;
            _snackbar.Show();
        }
    }
}