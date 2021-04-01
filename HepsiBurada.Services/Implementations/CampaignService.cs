using System;
using System.Linq;
using HepsiBurada.Data.Repositories.Interfaces;
using HepsiBurada.Domain;
using HepsiBurada.Domain.Dtos;
using HepsiBurada.Services.Interfaces;

namespace HepsiBurada.Services.Implementations
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _repository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public CampaignService(ICampaignRepository repository, IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _repository = repository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public string CreateCampaign(string[] commandArray)
        {
            try
            {
                var name = commandArray[1];
                var productCode = commandArray[2];
                var duration = commandArray[3];
                var limit = commandArray[4];
                var targetSalesCount = commandArray[5];

                var product = _productRepository.FindByCode(productCode);
                if (product == null) return "There are no product for this code.";

                var campaign = new Campaign
                {
                    Name = name,
                    ProductCode = productCode,
                    Duration = Convert.ToInt32(duration),
                    Limit = Convert.ToInt32(limit),
                    TargetSalesCount = Convert.ToInt32(targetSalesCount),
                    Active = true,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddHours(Convert.ToInt32(duration)),
                    Turnover = 0,
                    TotalSales = 0,
                    AverageItemPrice = 0,
                    CampaignProductPrice = product.Price
                };

                _repository.AddCampaign(campaign);
                return
                    $"Campaign created; name {campaign.Name}, product {campaign.ProductCode}, duration {campaign.Duration}, limit {campaign.Limit}, target sales count {campaign.TargetSalesCount}";
            }
            catch
            {
                return "create_campaign NAME PRODUCTCODE DURATION PMLIMIT TARGETSALESCOUNT";
            }
        }

        public string GetCampaignInfo(string[] commandArray)
        {
            try
            {
                var name = commandArray[1];

                var campaign = _repository.FindByName(name);
                var campaignDto = new CampaignDto
                {
                    Id = campaign.Id,
                    Name = campaign.Name,
                    Status = campaign.Active ? "Active" : "Ended",
                    TargetSales = campaign.TargetSalesCount,
                    TotalSales = campaign.TotalSales,
                    Turnover = campaign.Turnover,
                    AverageItemPrice = campaign.AverageItemPrice
                };
                return
                    $"Campaign {campaignDto.Name} info; Status {campaignDto.Status}, Target Sales {campaignDto.TargetSales}, Total Sales {campaignDto.TotalSales}, Turnover {campaignDto.Turnover}, Average Item Price {campaignDto.TargetSales}";
            }
            catch
            {
                return "get_campaign_info NAME";
            }
        }

        public void MakePassiveFinishedCampaigns()
        {
            var campaigns = _repository.FindActiveCampaigns();
            foreach (var campaign in campaigns)
            {
                var product = _productRepository.FindByCode(campaign.ProductCode);
                var soldProductsCount = _orderRepository.FindSoldProductCount(campaign.ProductCode);
                if (product.Stock.Equals(soldProductsCount) || campaign.Duration.Equals(0) ||
                    campaign.EndDate < DateTime.Now)
                    campaign.Active = false;
                _repository.UpdateCampaign(campaign);
            }
        }

        public bool IsExistActiveCampaign()
        {
            return _repository.FindActiveCampaigns().Any();
        }
    }
}