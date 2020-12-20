using System.Collections.Generic;
using System.Threading.Tasks;
using AutoStonks.SPA.Models;

namespace AutoStonks.SPA.Services.BrandService
{
    public interface IBrandService
    {
        Task<List<Brand>> GetBrands();
        Task<List<Model>> GetModelsForBrand(int brandId);
        Task<List<Brand>> GetGenerationsForModel(int modelId);
    }
}