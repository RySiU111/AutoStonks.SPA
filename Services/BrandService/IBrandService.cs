using System.Collections.Generic;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;

namespace AutoStonks.SPA.Services.BrandService
{
    public interface IBrandService
    {
        Task<ServiceResponse<List<Brand>>> GetBrands();
        Task<ServiceResponse<List<Model>>> GetModel();
        Task<ServiceResponse<List<Brand>>> GetGeneration();
    }
}