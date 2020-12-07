using System.Net.Http;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Linq;

namespace AutoStonks.SPA.Services
{
    public class AdvertService : IAdvertService
    {
        private HttpClient _http;
        private string _baseUrl = "http://localhost:5000/";

        public AdvertService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Advert>> GetAdverts()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Advert>>>($"{_baseUrl}advert/GetActiveBasic");

            System.Console.WriteLine(response.Data.Count());

            if(!response.Success)
                return null;

            return response.Data;
        }
    }
}