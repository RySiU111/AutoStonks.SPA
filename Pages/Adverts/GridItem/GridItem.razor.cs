using AutoStonks.SPA.Models;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Adverts.GridItem
{
    public partial class GridItem : ComponentBase
    {
        [Parameter]
        public Advert Advert { get; set; }
    }
}