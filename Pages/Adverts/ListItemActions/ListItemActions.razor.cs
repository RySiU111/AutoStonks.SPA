using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using AutoStonks.SPA.Services;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Adverts.ListItemActions
{
    public partial class ListItemActions : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAdvertService AdvertService { get; set; }
        
        [Parameter]
        public Advert Item { get; set; }

        [Parameter]
        public User User { get; set; }

        private void NavigateToDetailed() =>
            NavigationManager.NavigateTo($"detailed/{Item.Id}");

        private void NavigateToEdit() =>
            NavigationManager.NavigateTo($"/advert/form/{Item.Id}");         
    }
}