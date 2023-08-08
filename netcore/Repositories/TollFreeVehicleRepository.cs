using congestion.calculator.DbContexts;
using congestion.calculator.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace congestion.calculator.Repositories
{
    public class TollFreeVehicleRepository : ITollFreeVehicleRepository
    {
        private readonly CongestionTaxDbContext _dbContext;

        public TollFreeVehicleRepository(CongestionTaxDbContext congestionTaxDbContext)
        {
            _dbContext = congestionTaxDbContext ?? throw new ArgumentException(nameof(CongestionTaxDbContext));
        }

        public async Task<bool> IsExistAsync(int cityId, VehicleEnum vehicle)
        {
            return await _dbContext.TollFreeVehicles.AnyAsync(q => q.CityId == cityId && q.Vehicle == vehicle);
        }
    }
}
