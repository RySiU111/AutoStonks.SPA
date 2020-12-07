using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace AutoStonks.SPA.Pages.Adverts.List
{
    public partial class List : ComponentBase
    {
        [Inject]
        public IAdvertService AdvertService { get; set; }
        private List<Advert> _adverts;

        protected override async Task OnInitializedAsync()
        {
            // _adverts = await AdvertService.GetAdverts();
            var advert = new Advert()
            {
                Id = 1,
                CarProductionDate = new DateTime(1990,1,1),
                Mileage = 300000,
                Displacement = 3000,
                Fuel = FuelType.Petrol,
                Generation = new Generation
                {
                    Name = "mk3",
                    Model = new Model
                    {
                        Name = "Supra",
                        Brand = new Brand
                        {
                            Name = "Toyota"
                        }
                    }
                }
            };

            _adverts = new List<Advert>();
            _adverts.AddRange(Enumerable.Repeat(advert, 5));
        }
    }
}