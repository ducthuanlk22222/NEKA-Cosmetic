using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Orders;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Orders
{
    public interface IOrderService
    {
        Task<OrderVm> GetUserId(string languageId, int id);
        //Task<PagedResult<CategoryVm>> GetAllPaging(GetManageCategoryPagingRequest request);
        //Task<List<CategoryVm>> GetAll(string languageId);

        //Task<CategoryVm> GetById(string languageId, int id);
    }
}
