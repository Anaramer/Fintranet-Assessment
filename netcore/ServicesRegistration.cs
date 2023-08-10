using System;
using congestion.calculator.DbContexts;
using congestion.calculator.Repositories;
using congestion.calculator.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace congestion.calculator
{
    public static class ServicesRegistration
    {
        public static IServiceCollection ConfigureCongestionTaxServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CongestionTaxDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("CongestionConnectionString") 
                                  ?? throw new InvalidOperationException(nameof(CongestionTaxDbContext)));
            });

            services.AddScoped<ITollFeeRepository, TollFeeRepository>();
            services.AddScoped<ITollFreeDateRepository, TollFreeDateRepository>();
            services.AddScoped<ITollFreeDayOfWeekRepository, TollFreeDayOfWeekRepository>();
            services.AddScoped<ITollFreeMonthRepository, TollFreeMonthRepository>();
            services.AddScoped<ITollFreeVehicleRepository, TollFreeVehicleRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<ICongestionTaxCalculatorService, CongestionTaxCalculatorService>();

            return services;
        }
    }
}
