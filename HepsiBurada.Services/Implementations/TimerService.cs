using System;
using System.Timers;
using HepsiBurada.Services.Interfaces;

namespace HepsiBurada.Services.Implementations
{
    public class TimerService : ITimerService
    {
        private static Timer _timer;
        private readonly IOrderService _orderService;
        private readonly ICampaignService _campaignService;
        private readonly IAppTimeService _appTimeService;

        public TimerService(IOrderService orderService, ICampaignService campaignService, IAppTimeService appTimeService)
        {
            _orderService = orderService;
            _campaignService = campaignService;
            _appTimeService = appTimeService;
        }

        public void Start()
        {
            var timer = new Timer(10000);
            timer.Elapsed += _timer_Elapsed;
            timer.Enabled = true;
            _timer = timer;
            Console.WriteLine("timer start");
        }

        public void End()
        {
            if(_timer != null && _timer.Enabled)
                _timer.Close();
            Console.WriteLine("timer end");
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var result = _orderService.CreateOrder();
            Console.WriteLine(result);
            UpdateTurnover();
            _campaignService.MakePassiveFinishedCampaigns();
            if(!_campaignService.IsExistActiveCampaign()) End();
        }

        private void UpdateTurnover()
        {
            var appTime = _appTimeService.FindAppTime();
            appTime.TurnOver += 1;
            _appTimeService.UpdateAppTime(appTime);
        }
    }
}