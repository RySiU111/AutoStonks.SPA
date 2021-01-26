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
using System.Web;
using System.Reflection;

namespace AutoStonks.SPA.Services
{
    public class AdvertService : IAdvertService
    {
        private HttpClient _http;
        private string _baseUrl;

        public AdvertService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _baseUrl = config["ServiceUrl"];
        }

        public async Task<List<Advert>> FilterAdvert(FilterQuery filterQuery)
        {
            var builder = new UriBuilder($"{_baseUrl}advert/query");
            var query = HttpUtility.ParseQueryString(builder.Query);
            var properties = typeof(FilterQuery).GetProperties();
            var defaultFilters = new FilterQuery();
            
            foreach(var p in properties)
            {
                if(p.GetValue(filterQuery) != null &&
                    p.PropertyType.IsValueType &&
                    p.GetValue(filterQuery).GetHashCode() != 
                    p.GetValue(defaultFilters)?.GetHashCode())
                {
                    query[p.Name] = p.GetValue(filterQuery).ToString();
                }
                else if(p.PropertyType == typeof(string) && !String.IsNullOrEmpty((string)p.GetValue(filterQuery)))
                {
                    query[p.Name] = p.GetValue(filterQuery).ToString();
                }
            }

            builder.Query = query.ToString();

            ServiceResponse<List<Advert>> result;

            try
            {
                result = await _http.GetFromJsonAsync<ServiceResponse<List<Advert>>>(builder.ToString());
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }

            return result.Data;
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

        public async Task<List<Equipment>> GetEquipment()
        {
            ServiceResponse<List<Equipment>> response;

            try
            {
                response = await _http.GetFromJsonAsync<ServiceResponse<List<Equipment>>>($"{_baseUrl}equipment");
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

        public async Task<ServiceResponse<Advert>> PostAdvert(Advert advert)
        {
            if(advert == null)
                return new ServiceResponse<Advert>() { Success = false, Message = "Brak informacji o ogłoszeniu." };

            HttpResponseMessage response;

            try
            {
                response = await _http.PostAsync($"{_baseUrl}advert/", GetStringContent(advert));
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return new ServiceResponse<Advert>() { Success = false, Message = e.Message };;
            }
            
            return await ResponseToContent(response);
        }

        public async Task<ServiceResponse<Advert>> PutAdvert(Advert advert)
        {
            if(advert == null)
                return new ServiceResponse<Advert>() { Success = false, Message = "Brak informacji o ogłoszeniu." };

            HttpResponseMessage response;
            
            try
            {
                response = await _http.PutAsync($"{_baseUrl}advert/", GetStringContent(advert));
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return new ServiceResponse<Advert>() { Success = false, Message = e.Message };
            }

            if(!response.IsSuccessStatusCode)
                return new ServiceResponse<Advert>() { Success = false, Message = "Wewnętrzny błąd serwera." };

            return await ResponseToContent(response);
        }

        private StringContent GetStringContent(Advert advert) => 
            new StringContent(JObject.FromObject(advert).ToString(), Encoding.UTF8, "application/json");

        private async Task<ServiceResponse<Advert>> ResponseToContent(HttpResponseMessage response) =>
            JObject.Parse(await response.Content.ReadAsStringAsync()).ToObject<ServiceResponse<Advert>>();
    }
}