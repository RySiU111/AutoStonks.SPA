using AutoStonks.SPA.Models;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Adverts.ListItemDetails
{
    public partial class ListItemDetails : ComponentBase
    {
        [Parameter]
        public Advert Item { get; set; }
    }
}