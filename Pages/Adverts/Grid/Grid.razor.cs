using System.Collections.Generic;
using AutoStonks.SPA.Models;
using Microsoft.AspNetCore.Components;

namespace AutoStonks.SPA.Pages.Adverts.Grid
{
    public partial class Grid : ComponentBase
    {
        private List<Advert> _adverts = new List<Advert>()
        {
            new Advert() {Id = 1},
            new Advert() {Id = 2},
            new Advert() {Id = 3}
        };
    }
}