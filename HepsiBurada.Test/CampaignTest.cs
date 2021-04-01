using System;
using HepsiBurada.Data.Repositories.Interfaces;
using HepsiBurada.Domain;
using HepsiBurada.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HepsiBurada.Test
{
    public class CampaignTest
    {
        [Theory]
        [InlineData("C1", "P1", 5, 20, 100, 0)]
        public void Is_CampaignRepository_CreateCampaign_Return_CampaignInfo(string name, string code, int duration,
            int limit, int tsCount, int expectedResult)
        {
            TestStartup.ConfigureServices();

            var repository = TestStartup.ServiceProvider.GetService<ICampaignRepository>();
            var campaign = new Campaign
            {
                Name = name,
                ProductCode = code,
                Duration = duration,
                Limit = limit,
                TargetSalesCount = tsCount,
                Active = true,
                Turnover = 0,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(6),
                TotalSales = 0,
                AverageItemPrice = 0,
                CampaignProductPrice = 0
            };

            var result = repository.AddCampaign(campaign);
            Assert.NotEqual(expectedResult, result.Id);

            TestStartup.DisposeServices();
        }

        [Theory]
        [InlineData("create_campaign C1 P1 5 20 100",
            "create_campaign NAME PRODUCTCODE DURATION PMLIMIT TARGETSALESCOUNT")]
        public void Is_CampaignService_CreateCampaign_Return_CampaignInfo(string command, string expectedResult)
        {
            TestStartup.ConfigureServices();

            var service = TestStartup.ServiceProvider.GetService<ICampaignService>();
            var result = service.CreateCampaign(command.Split(' '));
            Assert.NotEqual(expectedResult, result);

            TestStartup.DisposeServices();
        }
        
        [Theory]
        [InlineData("create_campaign C1 P1 5 20 100", "get_campaign_info C1",
            "Campaign C1 info; Status Active, Target Sales 100, Total Sales 0, Turnover 0, Average Item Price 0")]
        public void Is_CampaignService_GetCampaignInfo_Return_CampaignInfo(string createComman, string getCommand, string expectedResult)
        {
            TestStartup.ConfigureServices();
            
            var service = TestStartup.ServiceProvider.GetService<ICampaignService>();
            service.CreateCampaign(createComman.Split(' '));
            var result = service.CreateCampaign(getCommand.Split(' '));
            Assert.NotEqual(expectedResult, result);

            TestStartup.DisposeServices();
        }
    }
}