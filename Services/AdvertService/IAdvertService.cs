using System.Collections.Generic;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;

namespace AutoStonks.SPA.Services
{
    public interface IAdvertService
    {
        Task<List<Advert>> GetAdverts();
        Task<List<Equipment>> GetEquipment();
        Task<ServiceResponse<Payment>> PostAdvert(Payment advert);
        Task<ServiceResponse<object>> PutAdvert(Advert advert);
        Task<Advert> GetAdvert(int id);
        Task<List<Advert>> FilterAdvert(FilterQuery filterQuery);
        Task<ServiceResponse<object>> PutPayment(Payment payment);
    }
}