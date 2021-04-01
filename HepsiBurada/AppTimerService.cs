using System.Timers;
using HepsiBurada.Domain;
using HepsiBurada.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HepsiBurada
{
    public class AppTimerService
    {
        static Timer _timer;

        public static void Start(AppTime appTime)
        {
            // Part 1: set up the timer for 2 seconds.
            var timer = new Timer(2000);
            // To add the elapsed event handler:
            // ... Type "_timer.Elapsed += " and press tab twice.
            timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            timer.Enabled = true;
            _timer = timer;
        }

        static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Part 2: add DateTime for each timer event.
            var orderService = Startup.ServiceProvider.GetService<IOrderService>();
            orderService.CreateOrder();
        }
    }
}