using eShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Carts
{
    public class CartVm
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { set; get; }
        public Product Products { get; set; }
        public AppUser AppUser { get; set; }
    }
}
