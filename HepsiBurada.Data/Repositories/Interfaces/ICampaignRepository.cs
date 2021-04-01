using System.Collections.Generic;
using HepsiBurada.Domain;

namespace HepsiBurada.Data.Repositories.Interfaces
{
    public interface ICampaignRepository
    {
        Campaign AddCampaign(Campaign campaign);
        Campaign FindByName(string name);
        IList<Campaign> FindActiveCampaigns();
        Campaign UpdateCampaign(Campaign campaign);
        Campaign FindRandomCampaign();
    }
}