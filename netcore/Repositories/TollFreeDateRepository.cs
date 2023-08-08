using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using congestion.calculator.DbContexts;
using congestion.calculator.Entities;
using Microsoft.EntityFrameworkCore;

namespace congestion.calculator.Repositories
{
    public class TollFreeDateRepository : ITollFreeDateRepository
    {
        private readonly CongestionTaxDbContext _dbContext;

        public TollFreeDateRepository(CongestionTaxDbContext congestionTaxDbContext)
        {
            _dbContext = congestionTaxDbContext ?? throw new ArgumentException(nameof(CongestionTaxDbContext));
        }

        public async Task<bool> IsExistAsync(DateTime date)
        {
            return await _dbContext.TollFreeDates.AnyAsync(q =>
                q.HolidayDate.Year == date.Year
                && q.HolidayDate.Month == date.Month
                && q.HolidayDate.Day == date.Day
                );
        }
    }
}
