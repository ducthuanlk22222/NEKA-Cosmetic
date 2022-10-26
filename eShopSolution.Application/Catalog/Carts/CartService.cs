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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Carts
{
    public class CartService : ICartService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public CartService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<int> Create(CartCreateRequest request)
        {
            var cart = new Cart()
            {
                Price = request.Price,
                Product = request.Products,
                Quantity = request.Quantity,
                DateCreated = DateTime.Now,
                UserId = request.AppUser.Id,
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

        public async Task<PagedResult<CartVm>> GetAllPaging(GetCartPagingRequest request)
        {
            //1. Select join
            var query = from c in _context.Carts
                        join ct in _context.Products on c.Id equals ct.Id 
                        //join cu in _context.Users on c.Id equals cu.Id into ccu
                        select new { c, ct };

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CartVm()
                {
                    //Price = x.p.Price,
                    //Products = x.pt.Id,
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
            cart.ProductId = request.Products.Id;

            return await _context.SaveChangesAsync();
        }

    }
}
