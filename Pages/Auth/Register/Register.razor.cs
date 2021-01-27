using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services.AuthService;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Auth.Register
{
    public partial class Register
    {
        [Inject]
        public IAuthService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private User _userCredentials = new User();
        private string _errorMessage;
        private Snackbar _snackbar;
        
        [Required]
        private string _confirmPassword;

        private async Task HandleValidSubmit()
        {
            if(_userCredentials.Password != _confirmPassword)
            {
                _userCredentials.Password = null;
                _confirmPassword = null;
                ShowErrorSnackbar("Podane hasła są nieprawidłowe.");
                return;
            }

            var response = await AuthService.Register(_userCredentials);

            if(response == null)
            {
                ShowErrorSnackbar("Błąd.");
                return;
            }

            if(!response.Success)
            {
                _userCredentials = new User();
                _confirmPassword = null;
                System.Console.WriteLine(response.Message);
                ShowErrorSnackbar(response.Message);
                return;
            }
            else
            {
                NavigationManager.NavigateTo("login");
            }
        }

        private void ShowErrorSnackbar(string message)
        {
            System.Console.WriteLine(message);
            _errorMessage = message;
            _snackbar.Interval = 30000;
            _snackbar.Location = SnackbarLocation.Right;
            _snackbar.Multiline = true;
            _snackbar.Show();
        }
    }
}