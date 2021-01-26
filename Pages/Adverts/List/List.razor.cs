using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services;
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
        private List<Advert> _adverts;
        private Modal _modal;
        private FilterQuery _filterQuery = new FilterQuery();

        protected override async Task OnInitializedAsync()
        {
            _adverts = await GetAdverts();
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
            _adverts = await GetAdverts();
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
                    _adverts = _adverts.OrderBy(a => a.CreationDate).ToList();
                    break;
                }
                case Sort.Data2: 
                {
                    _adverts = _adverts.OrderByDescending(a => a.CreationDate).ToList();
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