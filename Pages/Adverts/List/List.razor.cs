using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace AutoStonks.SPA.Pages.Adverts.List
{
    public partial class List : ComponentBase
    {
        private List<Advert> _adverts = new List<Advert>();

        protected override async Task OnInitializedAsync()
        {
            var advert = new Advert()
            {
                Id = 1,
                CarProductionDate = new DateTime(1990,1,1),
                Mileage = 300000,
                Displacement = 3000,
                Fuel = Fuel.Petrol,
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

            _adverts.AddRange(Enumerable.Repeat(advert, 5));
        }
    }
}