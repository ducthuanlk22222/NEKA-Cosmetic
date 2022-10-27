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
        public List<int> Product { get; set; } = new List<int>();
        public decimal Price { get; set; }
        public AppUser AppUser { get; set; }
    }
}
