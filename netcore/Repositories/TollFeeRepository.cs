using congestion.calculator.DbContexts;
using congestion.calculator.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace congestion.calculator.Repositories
{
    public class TollFeeRepository : ITollFeeRepository
    {
        private readonly CongestionTaxDbContext _dbContext;

        public TollFeeRepository(CongestionTaxDbContext congestionTaxDbContext)
        {
            _dbContext = congestionTaxDbContext ?? throw new ArgumentException(nameof(CongestionTaxDbContext));
        }

        public async Task<IReadOnlyList<TollFeeEntity>> GetAllByCityIdAsync(int cityId)
        {
            return await _dbContext.TollFees.Where(q => q.CityId == cityId).OrderBy(q => q.StartTimeOfDay).ToListAsync();
        }
    }
}
