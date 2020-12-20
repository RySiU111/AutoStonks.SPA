using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services;
using AutoStonks.SPA.Services.BrandService;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace AutoStonks.SPA.Pages.Adverts.Form
{
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
        private string _errorMessage;

        protected override async Task OnInitializedAsync()
        {
            if(Id <= 0)
            {
                _brands = await BrandService.GetBrands();
                _brands = _brands.OrderBy(b => b.Name).ToList();
                return;
            }

            var response = await AdvertService.GetAdvert(Id);

            if(response == null)
                return;

            _advert = response;
        }

        private void BrandChangedHandler()
        {

        }

        private async Task HandleValidSubmit()
        {
            // _advert.GenerationId = 1;
            _advert.UserId = 1;
            ServiceResponse<Advert> response;

            if(Id > 0)
                response = await AdvertService.PutAdvert(_advert);
            else
                response = await AdvertService.PostAdvert(_advert);

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
    }
}