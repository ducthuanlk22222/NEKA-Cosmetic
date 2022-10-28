using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Carts
{
    public class CartCreateRequest
    {
        public ProductVm Product { get; set; }
        public int Quantity { get; set; }
        public int ProductId { set; get; }
    }
}
