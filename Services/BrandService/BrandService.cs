using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace AutoStonks.SPA.Services.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public BrandService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _baseUrl = config["ServiceUrl"];
        }

        public async Task<List<Brand>> GetBrands()
        {
            var response = await _http.GetAsync($"{_baseUrl}brand");

            if(!response.IsSuccessStatusCode)
                return null;

            var jsonString = await response.Content.ReadAsStringAsync();

            if(String.IsNullOrEmpty(jsonString))
                return null;

            var serviceResponse = JObject.Parse(jsonString).ToObject<ServiceResponse<List<Brand>>>();
            
            return serviceResponse.Data;
        }

        public Task<List<Brand>> GetGenerationsForModel(int modelId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Model>> GetModelsForBrand(int brandId)
        {
            throw new System.NotImplementedException();
        }
    }
}