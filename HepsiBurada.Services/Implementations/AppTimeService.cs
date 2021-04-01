using System;
using System.Linq;
using HepsiBurada.Data.Repositories.Interfaces;
using HepsiBurada.Domain;
using HepsiBurada.Services.Interfaces;

namespace HepsiBurada.Services.Implementations
{
    public class AppTimeService : IAppTimeService
    {
        private readonly IAppTimeRepository _appTimeRepository;
        private readonly ICampaignRepository _campaignRepository;

        public AppTimeService(IAppTimeRepository appTimeRepository, ICampaignRepository campaignRepository)
        {
            _appTimeRepository = appTimeRepository;
            _campaignRepository = campaignRepository;
        }

        public AppTime AddAppTime(AppTime appTime)
        {
            return _appTimeRepository.AddAppTime(appTime);
        }

        public AppTime FindAppTime()
        {
            return _appTimeRepository.FindActiveAppTime();
        }
        
        public void UpdateAppTime(AppTime appTime)
        {
            _appTimeRepository.Update(appTime);
        }
        
        public string IncreaseTime(string[] commandArray)
        {
            try
            {
                var time = Convert.ToInt32(commandArray[1]);
                var appTime = _appTimeRepository.FindActiveAppTime();
                appTime.Duration += time;
                _appTimeRepository.Update(appTime);

                var campaigns = _campaignRepository.FindActiveCampaigns();

                if (!campaigns.Any()) return "There is no active campaign! Please add campaigns!";

                foreach (var campaign in campaigns)
                {
                    campaign.Duration -= time;
                    if (campaign.Duration.Equals(0))
                        campaign.Active = false;
                    else
                        campaign.CampaignProductPrice += campaign.CampaignProductPrice * campaign.Limit / 100;
                    _campaignRepository.UpdateCampaign(campaign);
                }

                return $"Time is {appTime.Duration}:00";
            }
            catch
            {
                return "increase_time HOUR";
            }
        }
    }
}