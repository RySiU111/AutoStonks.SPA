using System.Collections.Generic;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;

namespace AutoStonks.SPA.Services
{
    public interface IAdvertService
    {
         Task<List<Advert>> GetAdverts();
    }
}