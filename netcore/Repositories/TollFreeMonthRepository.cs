using congestion.calculator.DbContexts;
using congestion.calculator.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace congestion.calculator.Repositories
{
    public class TollFreeMonthRepository : ITollFreeMonthRepository
    {
        private readonly CongestionTaxDbContext _dbContext;

        public TollFreeMonthRepository(CongestionTaxDbContext congestionTaxDbContext)
        {
            _dbContext = congestionTaxDbContext ?? throw new ArgumentException(nameof(CongestionTaxDbContext));
        }

        public async Task<bool> IsExistAsync(int cityId, MonthEnum month)
        {
            return await _dbContext.TollFreeMonths.AnyAsync(q => q.CityId == cityId && q.Month == month);
        }
    }
}
