using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using Newtonsoft.Json.Linq;

namespace AutoStonks.SPA.Pages.Adverts.Form
{
    public partial class Form
    {
        private Advert _advert = new Advert();

        private async Task HandleValidSubmit()
        {
            System.Console.WriteLine(JObject.FromObject(_advert));

            //TODO: Advert Service - POST
        }
    }
}