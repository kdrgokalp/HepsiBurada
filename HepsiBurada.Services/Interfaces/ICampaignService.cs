
namespace HepsiBurada.Services.Interfaces
{
    public interface ICampaignService
    {
        string CreateCampaign(string[] commandLine);
        string GetCampaignInfo(string[] commandLine);
        void MakePassiveFinishedCampaigns();
        bool IsExistActiveCampaign();
    }
}