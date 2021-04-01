using System;
using System.Linq;
using HepsiBurada.Data.Repositories.Interfaces;
using HepsiBurada.Domain;
using HepsiBurada.Services.Interfaces;

namespace HepsiBurada.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAppTimeRepository _appTimeRepository;

        public OrderService(IOrderRepository repository, ICampaignRepository campaignRepository,
            IProductRepository productRepository, IAppTimeRepository appTimeRepository)
        {
            _repository = repository;
            _campaignRepository = campaignRepository;
            _productRepository = productRepository;
            _appTimeRepository = appTimeRepository;
        }

        public string CreateOrder(string[] commandArray)
        {
            try
            {
                var productCode = commandArray[1];
                var quantity = commandArray[1];

                var order = new Order
                {
                    ProductCode = productCode,
                    Quantity = Convert.ToInt32(quantity)
                };

                _repository.AddOrder(order);
                return $"Order created; product {order.ProductCode}, quantity {order.Quantity}";
            }
            catch
            {
                return "create_order PRODUCTCODE QUANTITY ";
            }
        }

        public string CreateOrder()
        {
            try
            {
                var orderCampaign = _campaignRepository.FindRandomCampaign();
                var orderProduct = _productRepository.FindByCode(orderCampaign.ProductCode);
                var soldProductsCount = _repository.FindSoldProductCount(orderProduct.ProductCode);
                var orderQuantity = new Random().Next(1, orderProduct.Stock - soldProductsCount);
                var order = new Order
                {
                    ProductCode = orderProduct.ProductCode,
                    Quantity = Convert.ToInt32(orderQuantity),
                    Price = orderCampaign.CampaignProductPrice
                };

                _repository.AddOrder(order);

                UpdateCampaignByOrder(orderCampaign);
                
                return $"Order created; product {order.ProductCode}, quantity {order.Quantity}, price {order.Price}";
            }
            catch
            {
                return "There is no active campaign";
            }
        }

        private void UpdateCampaignByOrder(Campaign orderCampaign)
        {
            orderCampaign.TotalSales = _repository.OrderCountByProductCode(orderCampaign.ProductCode);
            orderCampaign.Turnover = _appTimeRepository.FindActiveAppTime().TurnOver;
            orderCampaign.AverageItemPrice =
                _repository.OrderPricesByProductCode(orderCampaign.ProductCode).Average();

            _campaignRepository.UpdateCampaign(orderCampaign);
        }
    }
}