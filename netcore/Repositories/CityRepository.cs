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
    public class CityRepository : ICityRepository
    {
        private readonly CongestionTaxDbContext _dbContext;

        public CityRepository(CongestionTaxDbContext congestionTaxDbContext)
        {
            _dbContext = congestionTaxDbContext ?? throw new ArgumentException(nameof(CongestionTaxDbContext));
        }

        public async Task<CityEntity> GetCityByIdAsync(int id)
        {
            return await _dbContext.Cities.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _dbContext.Cities.AnyAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<CityEntity>> GetAllAsync()
        {
            return await _dbContext.Cities.ToListAsync();
        }
    }
}
