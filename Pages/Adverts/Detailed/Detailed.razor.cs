using System;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Adverts.Detailed
{
    public partial class Detailed : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        private Advert _advert = new Advert();
        private string selectedSlide = "1";

        protected override async Task OnInitializedAsync()
        {
            _advert = new Advert()
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
        }
    }
}