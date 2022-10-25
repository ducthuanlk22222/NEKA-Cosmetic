using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Orders
{
    public class GetManageOrderPagingRequest : PagingRequestBase
    {
        public int? UserId { get; set; }
    }
}
