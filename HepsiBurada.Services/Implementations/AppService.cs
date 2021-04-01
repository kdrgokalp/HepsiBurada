using System;
using System.Linq;
using HepsiBurada.Services.Interfaces;

namespace HepsiBurada.Services.Implementations
{
    public class AppService : IAppService
    {
        private readonly IProductService _productService;
        private readonly ICampaignService _campaignService;
        private readonly ITimerService _timerService;
        private readonly IAppTimeService _appTimeService;

        public AppService(IProductService productService, ICampaignService campaignService, ITimerService timerService, IAppTimeService appTimeService)
        {
            _productService = productService;
            _campaignService = campaignService;
            _timerService = timerService;
            _appTimeService = appTimeService;
        }

        public bool StartApp(string action)
        {
            var commandLine = Console.ReadLine();

            var commandArray = commandLine?.Split(' ');
            action = commandArray.FirstOrDefault();
            
            var result = true;

            switch (action?.ToLower())
            {
                case "create_product":
                    Console.WriteLine(_productService.CreateProduct(commandArray));
                    break;

                case "get_product_info":
                    Console.WriteLine(_productService.GetProductInfo(commandArray));
                    result = _campaignService.IsExistActiveCampaign();
                    break;

                case "create_campaign":
                    Console.WriteLine(_campaignService.CreateCampaign(commandArray));
                    _timerService.Start();
                    result = _campaignService.IsExistActiveCampaign();
                    break;

                case "get_campaign_info":
                    Console.WriteLine(_campaignService.GetCampaignInfo(commandArray));
                    result = _campaignService.IsExistActiveCampaign();
                    break;

                case "increase_time":
                    Console.WriteLine(_appTimeService.IncreaseTime(commandArray));
                    result = _campaignService.IsExistActiveCampaign();
                    break;

                case "exit":
                    _timerService.End();
                    Console.WriteLine("bye bye");
                    result = false;
                    break;
                default:
                    Console.WriteLine("Incorrect command usage!");
                    break;
            }

            return result;
        }
    }
}