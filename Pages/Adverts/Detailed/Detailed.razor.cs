using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Adverts.Detailed
{
    public partial class Detailed : ComponentBase
    {
        [Inject]
        public IAdvertService AdvertService { get; set; }

        [Parameter]
        public int Id { get; set; }

        private Advert _advert = new Advert();
        private string selectedSlide = "0";
        private bool _hideVIN = true;
        private bool _hidePhoneNumber = true;

        protected override async Task OnInitializedAsync()
        {
            _advert = await AdvertService.GetAdvert(Id);
        }
    }
}