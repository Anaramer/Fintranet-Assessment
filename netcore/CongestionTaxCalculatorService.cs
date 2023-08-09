using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using congestion.calculator;
using congestion.calculator.Entities;
using congestion.calculator.Enums;
using congestion.calculator.Repositories;

public class CongestionTaxCalculatorService
{
    private readonly ITollFeeRepository _tollFeeRepository;
    private readonly ITollFreeDayOfWeekRepository _tollFreeDayOfWeekRepository;
    private readonly ITollFreeVehicleRepository _tollFreeVehicleRepository;
    private readonly ITollFreeDateRepository _tollFreeDateRepository;
    private readonly ITollFreeMonthRepository _tollFreeMonthRepository;
    private readonly ICityRepository _cityRepository;

    public CongestionTaxCalculatorService(
        ITollFeeRepository tollFeeRepository,
        ITollFreeDayOfWeekRepository tollFreeDayOfWeekRepository,
        ITollFreeVehicleRepository tollFreeVehicleRepository,
        ITollFreeDateRepository tollFreeDateRepository,
        ITollFreeMonthRepository tollFreeMonthRepository,
        ICityRepository cityRepository)
    {
        _tollFeeRepository = tollFeeRepository;
        _tollFreeDayOfWeekRepository = tollFreeDayOfWeekRepository;
        _tollFreeVehicleRepository = tollFreeVehicleRepository;
        _tollFreeDateRepository = tollFreeDateRepository;
        _tollFreeMonthRepository = tollFreeMonthRepository;
        _cityRepository = cityRepository;
    }

    public async Task<int> GetTax(int cityId, VehicleEnum vehicle, DateTime[] dates)
    {
        if (!await _cityRepository.IsExistAsync(cityId))
        {
            //throw not found error
            return -1;
        }

        var city = await _cityRepository.GetCityByIdAsync(cityId);

        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = await GetTollFee(city.Id, date, vehicle);
            int tempFee =await GetTollFee(city.Id, intervalStart, vehicle);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMillies / 1000 / 60;

            if (minutes <= city.SingleChargePeriodMinute)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > city.MaxTaxInOneDay) totalFee = city.MaxTaxInOneDay;
        return totalFee;
    }

    private async Task<bool> IsTollFreeVehicle(int cityId, VehicleEnum vehicle)
    {
        return await _tollFreeVehicleRepository.IsExistAsync(cityId, vehicle);
    }

    public async Task<int> GetTollFee(int cityId, DateTime date, VehicleEnum vehicle)
    {
        if (await IsTollFreeDate(cityId,date) || await IsTollFreeVehicle(cityId,vehicle)) 
            return 0;

        int timeOfAction = (date.Hour * 60) + date.Minute;

        var tollFees = await _tollFeeRepository.GetAllByCityIdAsync(cityId);

        var tollFee = tollFees.FirstOrDefault(d => d.StartTimeOfDay > timeOfAction);

        return tollFee?.Fee ?? 0;
    }

    private async Task<bool> IsTollFreeDate(int cityId,DateTime date)
    {
        var isFreeDate = await _tollFreeDateRepository.IsExistAsync(date);
        var isFreeDayOfWeek = await _tollFreeDayOfWeekRepository.IsExistAsync(cityId, date.DayOfWeek);
        var isFreeMonth = await _tollFreeMonthRepository.IsExistAsync(cityId, (MonthEnum)date.Month);

        return isFreeDate || isFreeDayOfWeek || isFreeMonth;
    }
}