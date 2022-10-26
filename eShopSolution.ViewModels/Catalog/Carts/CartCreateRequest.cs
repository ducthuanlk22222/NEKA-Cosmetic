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
        public List<string> Product { get; set; } = new List<string>();
        public decimal Price { get; set; }
        public AppUser AppUser { get; set; }
    }
}
