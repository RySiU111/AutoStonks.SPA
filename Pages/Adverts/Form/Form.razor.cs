using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services;
using AutoStonks.SPA.Services.AuthService;
using AutoStonks.SPA.Services.BrandService;
using Blazorise;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace AutoStonks.SPA.Pages.Adverts.Form
{
    public class EquipmentActive
    {
        public Equipment Equipment { get; set; }
        public bool Active { get; set; }

        public EquipmentActive(Equipment e, bool a)
        {
            Equipment = e;
            Active = a;
        }
        public EquipmentActive() { }
    }

    public partial class Form
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IAdvertService AdvertService { get; set; }

        [Inject]
        public IBrandService BrandService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAuthService AuthService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public int SelectedBrandId 
        { 
            get => _selectedBrandId; 
            set
            {
                _selectedModelId = 0;
                _advert.GenerationId = 0;
                _selectedBrandId = value;
            } 
        }

        public int SelectedModelId 
        { 
            get => _selectedModelId; 
            set
            {
                _advert.GenerationId = 0;
                _selectedModelId = value;
            } 
        }

        private Advert _advert = new Advert()
        {
            FirstRegistrationDate = DateTime.Now
        };
        private List<Brand> _brands = new List<Brand>();
        private int _selectedBrandId;
        
        private int _selectedModelId;
        private Snackbar _snackbar;
        private Modal _equipmentModal;
        private string _errorMessage;
        private User _user;
        private List<EquipmentActive> _equipment = new List<EquipmentActive>();
        protected override async Task OnInitializedAsync()
        {
            var isAuthenticated = await CheckUser();

            if(!isAuthenticated)
                return;

            if(Id <= 0)
            {
                _brands = await BrandService.GetBrands();
                _brands = _brands.OrderBy(b => b.Name).ToList();
                await SetEquipment();

                return;
            }

            var response = await AdvertService.GetAdvert(Id);

            if(response == null)
                return;

            _advert = response;

            await SetEquipment();

            _brands = await BrandService.GetBrands();
            _brands = _brands.OrderBy(b => b.Name).ToList();

            if(_advert.Generation != null)
            {
                _selectedBrandId = _advert.Generation.Model.Brand.Id;
                _selectedModelId = _advert.Generation.Model.Id;
            }

            System.Console.WriteLine(JObject.FromObject(_advert));
        }

        private async Task HandleValidSubmit()
        {
            ServiceResponse<Advert> response;

            _advert.AdvertEquipments = _equipment
                .Where(e => e.Active)
                .Select(e => 
                    new AdvertEquipment()
                    {
                        AdvertId = _advert.Id,
                        EquipmentId = e.Equipment.Id
                    }
                ).ToList();

            if(Id > 0)
            {
                _advert.ModificationDate = DateTime.Now;
                response = await AdvertService.PutAdvert(_advert);
            }
            else
            {
                var isAuthenticated = await CheckUser();

                if(!isAuthenticated)
                    return;

                _advert.UserId = _user.Id;
                _advert.CreationDate = DateTime.Now;
                response = await AdvertService.PostAdvert(_advert);
            }
                

            System.Console.WriteLine(JObject.FromObject(_advert));
            if(!response.Success)
            {
                System.Console.WriteLine(response.Message);
                _errorMessage = response.Message;
                _snackbar.Interval = 15000;
                _snackbar.Location = SnackbarLocation.Right;
                _snackbar.Multiline = true;
                _snackbar.Show();
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }

        private async Task<bool> CheckUser()
        {
            if(_user == null)
            {
                _user = await AuthService.GetUser();

                if(_user == null)
                {
                    var redirectUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri).Replace('/','_');
                    NavigationManager.NavigateTo($"login/{redirectUrl}");
                    return false;
                }
            }

            return true;
        }

        private async Task SetEquipment()
        {
            var equipment = await AdvertService.GetEquipment();

            if(!equipment.Any())
                return;

            foreach(var eq in equipment)
            {
                if(_advert.AdvertEquipments != null && 
                    _advert.AdvertEquipments.Any(e => e.EquipmentId == eq.Id))
                    _equipment.Add(new EquipmentActive(eq, true));
                else
                    _equipment.Add(new EquipmentActive(eq, false));
            }

            _equipment = _equipment.OrderBy(e => e.Equipment.Name).ToList();
        }

        private void HideModal() => _equipmentModal.Hide();
        private void ShowModal() => _equipmentModal.Show();
    }
}