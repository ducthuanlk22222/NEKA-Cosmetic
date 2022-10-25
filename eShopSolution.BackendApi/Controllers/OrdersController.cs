using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Categories;
using eShopSolution.Application.Catalog.Orders;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _categoryService;

        public OrdersController(
            IOrderService categoryService)
        {
            _categoryService = categoryService;
        }

        //[HttpGet("paging")]
        //public async Task<IActionResult> GetAllPaging([FromQuery] GetManageOrderPagingRequest request)
        //{
        //    //var categories = await _categoryService.GetAllPaging(request);
        //    return Ok();
        //}
    }
}
