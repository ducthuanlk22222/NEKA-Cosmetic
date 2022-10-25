using eShopSolution.ViewModels.Catalog.Orders;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ApiIntegration
{
    public interface IOrderApiClient
    {
        Task<ApiResult<List<OrderVm>>> GetAll();
        Task<bool> CreateOrder(OrderCreateRequest request);
    }
}