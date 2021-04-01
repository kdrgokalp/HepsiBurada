using HepsiBurada.Domain;

namespace HepsiBurada.Data.Repositories.Interfaces
{
    public interface IAppTimeRepository
    {
        AppTime AddAppTime(AppTime appTime);
        AppTime FindActiveAppTime();
        void Update(AppTime appTime);
    }
}