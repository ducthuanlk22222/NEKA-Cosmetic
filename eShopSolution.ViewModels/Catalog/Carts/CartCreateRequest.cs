using eShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Carts
{
    public class CartCreateRequest
    {
        public int Quantity { get; set; }
        public Product Products { get; set; }
        public decimal Price { get; set; }
        public AppUser AppUser { get; set; }
    }
}
