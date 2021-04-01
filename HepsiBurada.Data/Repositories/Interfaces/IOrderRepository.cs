using System.Collections.Generic;
using HepsiBurada.Domain;

namespace HepsiBurada.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order AddOrder(Order order);
        int OrderCountByProductCode(string productCode);
        IList<double> OrderPricesByProductCode(string productCode);
        int FindSoldProductCount(string orderProductProductCode);
    }
}