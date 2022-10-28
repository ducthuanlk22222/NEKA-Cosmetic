using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Orders
{
    public class OrderService : IOrderService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public OrderService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public Order Create(ref Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _context.Add(order);
                _context.SaveChanges();

                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderId = order.Id;
                    _context.Add(orderDetail);
                }
                return order;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public void UpdateStatus(int orderId)
        //{
        //    var order = _context.GetById(orderId);
        //    order.Status = true;
        //    _context.Update(order);
        //}

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task<OrderVm> GetUserId(string languageId, int id)
        {
            throw new NotImplementedException();
        }
    }
}
