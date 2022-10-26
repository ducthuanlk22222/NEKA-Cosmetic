using eShopSolution.Application.Catalog.Orders;
using eShopSolution.Data.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Utilities;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly EShopDbContext _DbContext;
        public CartController(EShopDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        //[HttpGet["cart"]
        //public async Task<IActionResult>

    }
}
