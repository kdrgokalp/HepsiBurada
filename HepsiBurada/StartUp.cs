using System;
using HepsiBurada.Data.Context;
using HepsiBurada.Data.Repositories.Implementations;
using HepsiBurada.Data.Repositories.Interfaces;
using HepsiBurada.Services.Implementations;
using HepsiBurada.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HepsiBurada
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider;

        public static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<HepsiBuradaContext>(opt =>
                opt.UseInMemoryDatabase("HepsiBurada"));

            services.AddServices();

            ServiceProvider = services.BuildServiceProvider();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAppTimeRepository, AppTimeRepository>();
            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<IAppTimeService, AppTimeService>();
            services.AddScoped<ITimerService, TimerService>();
            services.AddScoped<IAppService, AppService>();
        }
        
        public static void DisposeServices()
        {
            if (ServiceProvider == null)
            {
                return;
            }

            if (ServiceProvider is IDisposable)
            {
                ((IDisposable) ServiceProvider).Dispose();
            }
        }
    }
}