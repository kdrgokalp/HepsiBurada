using HepsiBurada.Domain;

namespace HepsiBurada.Services.Interfaces
{
    public interface IAppTimeService
    {
        AppTime AddAppTime(AppTime appTime);
        void UpdateAppTime(AppTime appTime);
        string IncreaseTime(string[] commandArray);
        AppTime FindAppTime();
    }
}