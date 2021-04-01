using System.Linq;
using HepsiBurada.Data.Context;
using HepsiBurada.Data.Repositories.Interfaces;
using HepsiBurada.Domain;

namespace HepsiBurada.Data.Repositories.Implementations
{
    public class AppTimeRepository : IAppTimeRepository
    {
        private readonly HepsiBuradaContext _context;

        public AppTimeRepository(HepsiBuradaContext context) => _context = context;

        public AppTime AddAppTime(AppTime appTime)
        {
            _context.AppTimes.Add(appTime);
            _context.SaveChanges();
            return appTime;
        }

        public AppTime FindActiveAppTime()
        {
            return _context.AppTimes.FirstOrDefault(x => x.Active);
        }

        public void Update(AppTime appTime)
        {
            _context.AppTimes.Update(appTime);
            _context.SaveChanges();
        }
    }
}