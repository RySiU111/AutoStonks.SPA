using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services;
using AutoStonks.SPA.Services.AuthService;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Adverts.Detailed
{
    public partial class Detailed : ComponentBase
    {
        [Inject]
        public IAdvertService AdvertService { get; set; }

        [Inject]
        public IAuthService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int Id { get; set; }

        private Advert _advert = new Advert();
        private string selectedSlide = "0";
        private bool _hideVIN = true;
        private bool _hidePhoneNumber = true;
        private User _user = new User();

        protected override async Task OnInitializedAsync()
        {
            _advert = await AdvertService.GetAdvert(Id);
            var user = await AuthService.GetUser();
            _user = user ?? new User();
        }

        private async Task DeleteItem()
        {
            System.Console.WriteLine(_advert.Id);
            await AdvertService.DeleteAdvert(_advert.Id);
            NavigationManager.NavigateTo("/");
        }
    }
}