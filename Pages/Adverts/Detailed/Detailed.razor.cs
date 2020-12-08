using System;
using System.Collections.Generic;
using System.Linq;
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
        private string selectedSlide = "0";
        private bool _hideVIN = true;
        private bool _hidePhoneNumber = true;

        protected override async Task OnInitializedAsync()
        {
            _advert = new Advert()
            {
                Id = 1,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(14),
                IsPromoted = true,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin nibh augue, suscipit a, scelerisque sed, lacinia in, mi. Cras vel lorem. Etiam pellentesque aliquet tellus. Phasellus pharetra nulla ac diam. Quisque semper justo at risus. Donec venenatis, turpis vel hendrerit interdum, dui ligula ultricies purus, sed posuere libero dui id orci. Nam congue, pede vitae dapibus aliquet, elit magna vulputate arcu, vel tempus metus leo non est.",
                VIN = "2FMDK39C29BA10305",
                FirstRegistrationDate = new DateTime(1990,3,1),
                PlateNumber = "TYT4900",
                State = States.Active,
                Price = 67000.00,
                Mileage = 300000,
                CarProductionDate = new DateTime(1990,1,1),
                Fuel = FuelType.Petrol,
                Condition = ConditionState.Undamaged,
                Horsepower = 1000,
                Displacement = 3000,
                Location = "Poznań",
                HasBeenCrashed = false,
                TransmissionType = TransmissionTypes.Manual,
                DriveType = DriveTypes.RWD,
                VisitCount = 0,
                GenerationId = 1,
                Generation = new Generation
                {
                    Id = 1,
                    Name = "mk3",
                    Model = new Model
                    {
                        Id = 1,
                        Name = "Supra",
                        Brand = new Brand
                        {
                            Id = 1,
                            Name = "Toyota"
                        }
                    }
                },
                PhoneNumber = "123456789",
                Photos = new List<Photo>()
                {
                    new Photo()
                    {
                        Id = 1,
                        URL = "sample-data/assets/supramk3.jpg",
                    },
                    new Photo()
                    {
                        Id = 2,
                        URL = "sample-data/assets/256x256.png",
                    },
                },
                AdvertEquipments = new List<AdvertEquipment>()
                {
                    new AdvertEquipment()
                    {
                        EquipmentId = 1,
                        Equipment = new Equipment()
                        {
                            Id = 1,
                            Name = "Klimatyzacja"
                        }
                    },
                    new AdvertEquipment()
                    {
                        EquipmentId = 2,
                        Equipment = new Equipment()
                        {
                            Id = 2,
                            Name = "Alufelgi"
                        }
                    }
                }
            };
        }
    }
}