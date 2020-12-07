using AutoStonks.SPA.Models;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Adverts.ListItemActions
{
    public partial class ListItemActions : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public Advert Item { get; set; }

        private void NavigateToDetailed()
        {
            NavigationManager.NavigateTo($"detailed/{Item.Id}");
        }
    }
}