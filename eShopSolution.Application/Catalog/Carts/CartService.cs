using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Catalog.Carts;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Carts
{
    public class CartService : ICartService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<AppUser> _userManager;

        public CartService(EShopDbContext context,
            IStorageService storageService,
            IHttpContextAccessor httpContext,
            UserManager<AppUser> userManager)
        {
            _httpContext = httpContext;
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }

        public async Task<int> Create(CartCreateRequest request)
        {
            var userId = _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            //var product = _context.ProductTranslations.Where(x => request.Product.Contains(x.Id)).Select(x=>x.Id).ToList();
            //var producta = _context.ProductInCarts.Where(x => product.Contains(x.ProductId)).ToList();
            var cart = new Cart()
            {
                //Price = request.Price,
                Quantity = request.Quantity,
                DateCreated = DateTime.Now,
                UserId = new Guid(userId)
                //ProductInCarts= producta
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart.Id;
        }

        public async Task<int> Delete(int cartID)
        {
            var cart = await _context.Carts.FindAsync(cartID);
            if (cart == null) throw new EShopException($"Cannot find a cart: {cartID}");

            _context.Carts.Remove(cart);

            return await _context.SaveChangesAsync();
        }

        public async Task<ApiResult<bool>> CartAssign(Guid id, CartAssignRequest request)
        {
            var userId = await _context.Users.FindAsync(id);
            if (userId == null)
            {
                return new ApiErrorResult<bool>($"Giỏ hàng với id {id} không tồn tại");
            }
            return new ApiErrorResult<bool>();
        }
        public async Task<PagedResult<CartVm>> GetAllPaging(GetCartPagingRequest request)
        {
            //1. Select join
            var query = from c in _context.Carts
                        join ct in _context.ProductInCarts on c.Id equals ct.CartId
                        join cu in _context.ProductInCarts on c.Id equals cu.ProductId
                        select new { c, ct, cu };

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CartVm()
                {
                    //Price = x.p.Price,
                    //Quantity = x.p.Quantity,
                    DateCreated = DateTime.Now,
                    //AppUser = x.cu,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<CartVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<int> Update(CartUpdateRequest request)
        {
            var cart = await _context.Carts.FindAsync(request.Id);

            if (cart == null) throw new EShopException($"Cannot find a cart with id: {request.Id}");

            cart.Quantity = request.Quantity;
            cart.UserId = request.AppUser.Id;
            cart.Price = request.Price;

            return await _context.SaveChangesAsync();
        }


    }
}
