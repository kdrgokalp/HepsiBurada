using System;
using HepsiBurada.Domain;
using HepsiBurada.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HepsiBurada
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup.ConfigureServices();

            var appTime = new AppTime
            {
                Active = true,
                Duration = 0,
                TurnOver = 0
            };
            var appTimeService = Startup.ServiceProvider.GetService<IAppTimeService>();
            appTimeService.AddAppTime(appTime);

            var appService = Startup.ServiceProvider.GetService<IAppService>();

            var action = String.Empty;
            do
            {
                appService.StartApp(action);
                appTime.TurnOver += 1;
                appTimeService.UpdateAppTime(appTime);
            } while (action != "exit");

            Startup.DisposeServices();
        }
    }
}