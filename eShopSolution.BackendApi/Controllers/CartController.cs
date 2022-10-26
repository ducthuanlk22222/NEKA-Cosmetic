using eShopSolution.Application.Catalog.Carts;
using eShopSolution.Application.Catalog.Orders;
using eShopSolution.Application.Catalog.Products;
using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Carts;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Utilities;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly EShopDbContext _DbContext;
        private readonly ICartService _cartService;
        public CartController(EShopDbContext dbContext, ICartService cartService)
        {
            _DbContext = dbContext;
            _cartService = cartService;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetCartPagingRequest request)
        {
            var products = await _cartService.GetAllPaging(request);
            return Ok(products);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CartCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cartID = await _cartService.Create(request);
            if (cartID == 0)
                return BadRequest();

            return Ok();
        }
    }
}
