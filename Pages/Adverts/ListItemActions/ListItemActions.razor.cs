using AutoStonks.SPA.Models;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Adverts.ListItemActions
{
    public partial class ListItemActions : ComponentBase
    {
        [Parameter]
        public Advert Item { get; set; }
    }
}