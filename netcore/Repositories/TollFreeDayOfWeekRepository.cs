using congestion.calculator.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace congestion.calculator.Repositories
{
    public class TollFreeDayOfWeekRepository : ITollFreeDayOfWeekRepository
    {
        private readonly CongestionTaxDbContext _dbContext;

        public TollFreeDayOfWeekRepository(CongestionTaxDbContext congestionTaxDbContext)
        {
            _dbContext = congestionTaxDbContext ?? throw new ArgumentException(nameof(CongestionTaxDbContext));
        }

        public async Task<bool> IsExistAsync(int cityId, DayOfWeek dayOfWeek)
        {
            return await _dbContext.TollFreeDayOfWeeks.AnyAsync(q => q.CityId == cityId && q.DayOfWeek == dayOfWeek);
        }
    }
}
