using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Utilities.Slides;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public interface ISlideApiClient
    {
        Task<List<SlideVm>> GetAll();
        Task<SlideVm> GetById(int id);
        Task<PagedResult<SlideVm>> GetPagings(GetSlidePagingRequest request);

        Task<bool> CreateSlides(SlideCreateRequest request);

        Task<bool> UpdateSlides(SlideUpdateRequest request);

        Task<bool> DeleteSlides(int request);
    }
}