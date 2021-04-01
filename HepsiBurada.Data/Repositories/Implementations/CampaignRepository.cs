using System;
using System.Collections.Generic;
using System.Linq;
using HepsiBurada.Data.Context;
using HepsiBurada.Data.Repositories.Interfaces;
using HepsiBurada.Domain;

namespace HepsiBurada.Data.Repositories.Implementations
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly HepsiBuradaContext _context;

        public CampaignRepository(HepsiBuradaContext context) => _context = context;

        public Campaign AddCampaign(Campaign campaign)
        {
            _context.Campaigns.Add(campaign);
            _context.SaveChanges();
            return campaign;
        }

        public Campaign FindByName(string name)
        {
            return _context.Campaigns.FirstOrDefault(x => x.Name.ToLower().Contains(name.ToLower()) && x.Active);
        }

        public IList<Campaign> FindActiveCampaigns()
        {
            return _context.Campaigns.Where(x => x.Active && x.Duration > 0 && x.EndDate >= DateTime.Now).ToList();
        }

        public Campaign UpdateCampaign(Campaign campaign)
        {
            _context.Campaigns.Update(campaign);
            _context.SaveChanges();
            return campaign;
        }

        public Campaign FindRandomCampaign()
        {
            var campaigns = FindActiveCampaigns();
            var random = new Random().Next(0, campaigns.Count - 1);
            return campaigns[random];
        }
    }
}