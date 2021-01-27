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

        public async Task<ServiceResponse<Payment>> PostAdvert(Payment payment)
        {
            if(payment == null)
                return new ServiceResponse<Payment>() { Success = false, Message = "Brak informacji o ogłoszeniu." };

            HttpResponseMessage response;

            try
            {
                response = await _http.PostAsync($"{_baseUrl}advert/", GetStringContent(payment));
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return new ServiceResponse<Payment>() { Success = false, Message = e.Message };;
            }
            
            return await ResponseToContent<Payment>(response);
        }

        public async Task<ServiceResponse<object>> PutAdvert(Advert advert)
        {
            if(advert == null)
                return new ServiceResponse<object>() { Success = false, Message = "Brak informacji o ogłoszeniu." };

            HttpResponseMessage response;
            
            try
            {
                response = await _http.PutAsync($"{_baseUrl}advert/", GetStringContent(advert));
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return new ServiceResponse<object>() { Success = false, Message = e.Message };
            }

            if(!response.IsSuccessStatusCode)
                return new ServiceResponse<object>() { Success = false, Message = "Wewnętrzny błąd serwera." };

            return await ResponseToContent<object>(response);
        }

        public async Task<ServiceResponse<object>> PutPayment(Payment payment)
        {
            if(payment == null)
                return new ServiceResponse<object>() { Success = false, Message = "Brak informacji o ogłoszeniu." };

            HttpResponseMessage response;
            
            try
            {
                var content = JObject.FromObject(payment);
                content.Add("isTerminated", false);
                var stringContent =  new StringContent(content.ToString(), Encoding.UTF8, "application/json");
                
                response = await _http.PutAsync($"{_baseUrl}advert/", stringContent);
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
                return new ServiceResponse<object>() { Success = false, Message = e.Message };
            }

            if(!response.IsSuccessStatusCode)
                return new ServiceResponse<object>() { Success = false, Message = "Wewnętrzny błąd serwera." };

            return await ResponseToContent<object>(response);
        }

        private StringContent GetStringContent<T>(T obj) => 
            new StringContent(JObject.FromObject(obj).ToString(), Encoding.UTF8, "application/json");

        private async Task<ServiceResponse<T>> ResponseToContent<T>(HttpResponseMessage response) =>
            JObject.Parse(await response.Content.ReadAsStringAsync()).ToObject<ServiceResponse<T>>();

        // private async Task<ServiceResponse<object>> ResponseToContentString(HttpResponseMessage response) =>
        //     JObject.Parse(await response.Content.ReadAsStringAsync()).ToObject<ServiceResponse<object>>();
    }
}