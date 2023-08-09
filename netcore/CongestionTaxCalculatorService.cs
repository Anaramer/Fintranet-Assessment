using congestion.calculator.Enums;
using congestion.calculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace congestion.calculator
{
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

            var feeForDate = await GetTaxOfDateWithDetail(cityId, vehicle, dates);

            Dictionary<DateTime, int> taxOfDays = new Dictionary<DateTime, int>();

            foreach (var item in feeForDate.OrderBy(q => q.Key))
            {
                DateTime dateTimeClear = new DateTime(item.Key.Year, item.Key.Month, item.Key.Day);
                if (taxOfDays.Keys.FirstOrDefault(q => q == dateTimeClear) == default)
                {
                    taxOfDays.Add(item.Key, 0);
                }

                taxOfDays[dateTimeClear] += item.Value;

                if (taxOfDays[dateTimeClear] > city.MaxTaxInOneDay)
                    taxOfDays[dateTimeClear] = city.MaxTaxInOneDay;
            }

            return taxOfDays.Sum(q => q.Value);
        }

        public async Task<Dictionary<DateTime, int>> GetTaxOfDateWithDetail(int cityId, VehicleEnum vehicle, DateTime[] dates)
        {
            if (!await _cityRepository.IsExistAsync(cityId))
            {
                //throw not found error
                return null;
            }

            var city = await _cityRepository.GetCityByIdAsync(cityId);

            Dictionary<DateTime, int> resultFeeForDate = new Dictionary<DateTime, int>();

            foreach (var date in dates.OrderBy(q => q.Date))
            {
                if (resultFeeForDate.Keys.FirstOrDefault(q => q == date) == default)
                {
                    resultFeeForDate.Add(date, 0);
                }

                var lastBefore = resultFeeForDate.Keys.LastOrDefault(q => q <= date);
                long diffTime = (date.Millisecond - lastBefore.Millisecond) / (1000 * 60); // as minute

                var fee = await GetTollFee(city.Id, date, vehicle);

                if (date.Year == lastBefore.Year
                    && date.Month == lastBefore.Month
                    && date.Day == lastBefore.Day // check to be in the same day
                    && diffTime < city.SingleChargePeriodMinute)
                {
                    if (resultFeeForDate[date] < fee)
                        resultFeeForDate[date] = fee;
                }
                else
                {
                    resultFeeForDate[date] = fee;
                }
            }

            return resultFeeForDate;
        }

        public async Task<int> GetTollFee(int cityId, DateTime date, VehicleEnum vehicle)
        {
            if (await IsTollFreeDate(cityId, date) || await IsTollFreeVehicle(cityId, vehicle))
                return 0;

            int timeOfAction = (date.Hour * 60) + date.Minute;

            var tollFees = await _tollFeeRepository.GetAllByCityIdAsync(cityId);

            var tollFee = tollFees.FirstOrDefault(d => d.StartTimeOfDay > timeOfAction);

            return tollFee?.Fee ?? 0;
        }

        private async Task<bool> IsTollFreeDate(int cityId, DateTime date)
        {
            var isFreeDate = await _tollFreeDateRepository.IsExistAsync(date);
            var isFreeDayOfWeek = await _tollFreeDayOfWeekRepository.IsExistAsync(cityId, date.DayOfWeek);
            var isFreeMonth = await _tollFreeMonthRepository.IsExistAsync(cityId, (MonthEnum)date.Month);

            return isFreeDate || isFreeDayOfWeek || isFreeMonth;
        }

        private async Task<bool> IsTollFreeVehicle(int cityId, VehicleEnum vehicle)
        {
            return await _tollFreeVehicleRepository.IsExistAsync(cityId, vehicle);
        }
    }
}