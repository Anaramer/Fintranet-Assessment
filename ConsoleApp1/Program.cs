using congestion.calculator;
using congestion.calculator.DTOs;
using congestion.calculator.Enums;
using congestion.calculator.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.ConfigureCongestionTaxServices(hostContext.Configuration);
        services.AddSingleton<CongestionTaxCalculatorService>();
    })
    .Build();

var my = host.Services.GetRequiredService<CongestionTaxCalculatorService>();

Console.Clear();

Console.WriteLine("----------------------GetAllCities----------------------------");

foreach (CityDto city in await my.GetAllCities())
{
    Console.WriteLine($"City : [{city.Id}] {city.Name} - {city.MaxTaxInOneDay} SEK - {city.SingleChargePeriodMinute} Min");
}

Console.WriteLine("----------------------GetTaxOfDateWithDetail----------------------------");

var dateOfEnter = new DateTime[]
{
    DateTime.Parse("2013-01-14 21:00:00"),
    DateTime.Parse("2013-01-15 21:00:00"),
    DateTime.Parse("2013-02-07 06:23:27"),
    DateTime.Parse("2013-02-07 15:27:00"),
    DateTime.Parse("2013-02-08 06:27:00"),
    DateTime.Parse("2013-02-08 06:20:27"),
    DateTime.Parse("2013-02-08 14:35:00"),
    DateTime.Parse("2013-02-08 15:29:00"),
    DateTime.Parse("2013-02-08 15:47:00"),
    DateTime.Parse("2013-02-08 16:01:00"),
    DateTime.Parse("2013-02-08 16:48:00"),
    DateTime.Parse("2013-02-08 17:49:00"),
    DateTime.Parse("2013-02-08 18:29:00"),
    DateTime.Parse("2013-02-08 18:35:00"),
    DateTime.Parse("2013-03-26 14:25:00"),
    DateTime.Parse("2013-03-28 14:07:27") // Free Date
};

foreach (var item in await my.GetTaxOfDateWithDetail(1,VehicleEnum.Car,dateOfEnter))
{
    Console.WriteLine($"{item.Key} = {item.Value} SEK");
}

Console.WriteLine($" Sum Tax = { await my.GetTax(1, VehicleEnum.Car, dateOfEnter)} SEK");


await host.RunAsync();