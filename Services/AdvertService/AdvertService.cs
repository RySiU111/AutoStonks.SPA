using System.Net.Http;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;

namespace AutoStonks.SPA.Services
{
    public class AdvertService : IAdvertService
    {
        private HttpClient _http;
        private string _baseUrl = "http://localhost:5000/";

        public AdvertService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _baseUrl = config["ServiceUrl"];
        }

        public async Task<Advert> GetAdvert(int id)
        {
            if(id < 0)
                return null;

            ServiceResponse<Advert> response;

            try
            {
                response = await _http.GetFromJsonAsync<ServiceResponse<Advert>>($"{_baseUrl}advert/GetFullInfo={id}");
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }

            if(!response.Success)
                return null;

            return response.Data;
        }

        public async Task<List<Advert>> GetAdverts()
        {
            ServiceResponse<List<Advert>> response;

            try
            {
                response = await _http.GetFromJsonAsync<ServiceResponse<List<Advert>>>($"{_baseUrl}advert/GetActiveBasic");
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }

            if(!response.Success)
                return null;

            return response.Data;
        }

        public async Task<ServiceResponse<List<Advert>>> PostAdvert(Advert advert)
        {
            if(advert == null)
                return null;

            HttpResponseMessage response;

            try
            {
                response = await _http.PostAsync($"{_baseUrl}advert/", GetStringContent(advert));
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }

            return await ResponseToContent(response);
        }

        public async Task<ServiceResponse<List<Advert>>> PutAdvert(Advert advert)
        {
            if(advert == null)
                return null;

            HttpResponseMessage response;
            
            try
            {
                response = await _http.PutAsync($"{_baseUrl}advert/", GetStringContent(advert));
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }

            return await ResponseToContent(response);
        }

        private StringContent GetStringContent(Advert advert) => 
            new StringContent(JObject.FromObject(advert).ToString(), Encoding.UTF8, "application/json");

        private async Task<ServiceResponse<List<Advert>>> ResponseToContent(HttpResponseMessage response) =>
            JObject.Parse(await response.Content.ReadAsStringAsync()).ToObject<ServiceResponse<List<Advert>>>();
    }
}