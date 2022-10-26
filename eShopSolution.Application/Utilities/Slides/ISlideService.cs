using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Roles;
using eShopSolution.ViewModels.Utilities.Slides;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Utilities.Slides
{
    public interface ISlideService
    {
        Task<List<SlideVm>> GetAll();

        Task<int> Create(SlideCreateRequest request);

        Task<int> Update(SlideUpdateRequest request);

        Task<int> Delete(int request);
        Task<SlideVm> GetById(int id);

        Task<PagedResult<SlideVm>> GetAllPaging(GetSlidePagingRequest request);

    }
}