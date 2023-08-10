using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using congestion.calculator.DTOs;
using congestion.calculator.Enums;

namespace congestion.calculator.Services
{
    public interface ICongestionTaxCalculatorService
    {
        Task<int> GetTax(int cityId, VehicleEnum vehicle, DateTime[] dates);
        Task<Dictionary<DateTime, int>> GetTaxOfDateWithDetail(int cityId, VehicleEnum vehicle, DateTime[] dates);
        Task<IEnumerable<CityDto>> GetAllCities();
    }
}
