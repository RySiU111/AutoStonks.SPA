using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services;
using AutoStonks.SPA.Services.AuthService;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace AutoStonks.SPA.Pages.Adverts.List
{
    public enum Sort
    {
        Data1, Data2, Price1, Price2, Popular
    }

    public partial class List : ComponentBase
    {
        [Inject]
        public IAdvertService AdvertService { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Parameter]
        public List<Advert> Adverts { get; set; } = new List<Advert>();
        private List<Advert> _adverts;
        private Modal _modal;
        private FilterQuery _filterQuery = new FilterQuery();
        private User _user;

        protected override async Task OnInitializedAsync()
        {
            if(Adverts.Count == 0)
                _adverts = await GetAdverts();
            else 
                _adverts = Adverts;

            _user = await AuthService.GetUser();
        }

        private async Task<List<Advert>> GetAdverts()
        {
            var result = await AdvertService.GetAdverts();
            // if(result != null)
            //     result.ForEach(r => 
            //         r.Photos = new List<Photo>()
            //         {
            //             new Photo()
            //             {
            //                 Source = "sample-data/assets/supramk3.jpg"
            //             }
            //         });
            
            return result?.OrderBy(r => r.CreationDate).ToList();
        }

        private void ShowFilters()
        {
            _modal.Show();
        }

        private void HideFilters()
        {
            _modal.Hide();
        }

        private async Task SaveFilters()
        {
            var result = await AdvertService.FilterAdvert(_filterQuery);
            _adverts = result;

            _modal.Hide();
        }

        private async Task ClearFilters()
        {
            _filterQuery = new FilterQuery();
            
            if(Adverts.Count == 0)
                _adverts = await GetAdverts();
            else 
                _adverts = Adverts;
        }

        private void SortList(Sort sort)
        {
            switch(sort)
            {
                case Sort.Price1: 
                {
                    _adverts = _adverts.OrderBy(a => a.Price).ToList();
                    break;
                }
                case Sort.Price2: 
                {
                    _adverts = _adverts.OrderByDescending(a => a.Price).ToList();
                    break;
                }
                case Sort.Data1: 
                {
                    _adverts = _adverts.OrderBy(a => a.CarProductionDate).ToList();
                    break;
                }
                case Sort.Data2: 
                {
                    _adverts = _adverts.OrderByDescending(a => a.CarProductionDate).ToList();
                    break;
                }
                case Sort.Popular: 
                {
                    _adverts = _adverts.OrderByDescending(a => a.VisitCount).ToList();
                    break;
                }
            }
        }
    }
}