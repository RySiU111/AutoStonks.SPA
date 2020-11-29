using AutoStonks.SPA.Models;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Adverts.ListItem
{
    public partial class ListItem : ComponentBase
    {
        [Parameter]
        public Advert Item { get; set; }
    }
}