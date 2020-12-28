using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;

namespace AutoStonks.SPA.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;
        private readonly IJSRuntime _jsRuntime;

        public AuthService(HttpClient http, IConfiguration config, IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _http = http;
            _baseUrl = config["ServiceUrl"];
        }

        public async Task<User> GetUser()
        {
            var userJson = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "user");

            if(String.IsNullOrEmpty(userJson))
                return null;

            var user = JObject.Parse(userJson).ToObject<User>();

            return user;
        }

        public async Task<ServiceResponse<User>> Login(User credentials)
        {
            if (credentials == null)
                return null;

            HttpResponseMessage response;

            try
            {
                response = await _http.PostAsync($"{_baseUrl}user/login", GetStringContent(credentials));
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }

            var content = await ResponseToContent(response);

            if(content.Success)
                await SaveUser(content.Data);

            return content;
        }

        public async Task<ServiceResponse<User>> Register(User user)
        {
            if (user == null)
                return null;

            HttpResponseMessage response;

            try
            {
                response = await _http.PostAsync($"{_baseUrl}user", GetStringContent(user));
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await ResponseToContent(response);

            return content;
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "user");
        }

        private async Task SaveUser(User user) =>
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "user", JObject.FromObject(user).ToString());

        private StringContent GetStringContent(User user) =>
            new StringContent(JObject.FromObject(user).ToString(), Encoding.UTF8, "application/json");

        private async Task<ServiceResponse<User>> ResponseToContent(HttpResponseMessage response) =>
            JObject.Parse(await response.Content.ReadAsStringAsync()).ToObject<ServiceResponse<User>>();
    }
}