using System.Collections.Generic;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Adverts.Promoted
{
    public partial class Promoted : ComponentBase
    {
        [Inject]
        public IAdvertService AdvertService { get; set; }

        private List<Advert> _adverts = new List<Advert>();

        protected override async Task OnInitializedAsync()
        {
            _adverts = await AdvertService.GetPromotedAdverts();
        }
    }
}