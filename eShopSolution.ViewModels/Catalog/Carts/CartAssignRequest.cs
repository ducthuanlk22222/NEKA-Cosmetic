using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Carts
{
    public class CartAssignRequest
    {
        public int ID { get; set; }

        public List<SelectItem> Products { get; set; } = new List<SelectItem>();
    }
}
