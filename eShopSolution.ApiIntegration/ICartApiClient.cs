using eShopSolution.ViewModels.Catalog.Carts;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public interface ICartApiClient
    {
        Task<PagedResult<CartVm>> GetPagings(GetCartPagingRequest request);

        //Task<ApiResult<UserVm>> GetById(Guid id);
    }
}
