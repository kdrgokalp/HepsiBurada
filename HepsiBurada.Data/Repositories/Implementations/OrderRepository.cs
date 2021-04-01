using System.Collections.Generic;
using System.Linq;
using HepsiBurada.Data.Context;
using HepsiBurada.Data.Repositories.Interfaces;
using HepsiBurada.Domain;

namespace HepsiBurada.Data.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HepsiBuradaContext _context;

        public OrderRepository(HepsiBuradaContext context) => _context = context;

        public Order AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public int OrderCountByProductCode(string productCode)
        {
            return _context.Orders.Count(x => x.ProductCode.ToLower().Contains(productCode.ToLower()));
        }

        public IList<double> OrderPricesByProductCode(string productCode)
        {
            return _context.Orders.Where(x => x.ProductCode.ToLower().Contains(productCode.ToLower()))
                .Select(x => x.Price).ToList();
        }

        public int FindSoldProductCount(string productCode)
        {
            var orders = _context.Orders.Where(x => x.ProductCode.ToLower().Contains(productCode.ToLower())).ToList();
            return orders.Sum(x => x.Quantity);
        }
    }
}