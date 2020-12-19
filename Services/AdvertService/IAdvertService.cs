using System.Collections.Generic;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;

namespace AutoStonks.SPA.Services
{
    public interface IAdvertService
    {
        Task<List<Advert>> GetAdverts();
        Task<ServiceResponse<List<Advert>>> PostAdvert(Advert advert);
        Task<ServiceResponse<List<Advert>>> PutAdvert(Advert advert);
        Task<Advert> GetAdvert(int id);
    }
}