using System.Collections.Generic;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;

namespace AutoStonks.SPA.Services
{
    public interface IAdvertService
    {
        Task<List<Advert>> GetAdverts();
        Task<ServiceResponse<Advert>> PostAdvert(Advert advert);
        Task<ServiceResponse<Advert>> PutAdvert(Advert advert);
        Task<Advert> GetAdvert(int id);
    }
}