using congestion.calculator.DbContexts;
using congestion.calculator.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace congestion.calculator
{
    public static class ServicesRegistration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CongestionTaxDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("CongestionConnectionString"));
            });

            services.AddScoped<ITollFeeRepository, TollFeeRepository>();
            services.AddScoped<ITollFreeDateRepository, TollFreeDateRepository>();
            services.AddScoped<ITollFreeDayOfWeekRepository, TollFreeDayOfWeekRepository>();
            services.AddScoped<ITollFreeMonthRepository, TollFreeMonthRepository>();
            services.AddScoped<ITollFreeVehicleRepository, TollFreeVehicleRepository>();

            return services;
        }
    }
}
