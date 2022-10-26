using eShopSolution.ViewModels.Catalog.Carts;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Carts
{
    public interface ICartService
    {
        Task<int> Create(CartCreateRequest request);

        Task<int> Update(CartUpdateRequest request);

        Task<int> Delete(int cartID);
        Task<ApiResult<bool>> CategoryAssign(int id, CartAssignRequest request);
        Task<PagedResult<CartVm>> GetAllPaging(GetCartPagingRequest request);
    }
}
