using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using congestion.calculator.Entities;

namespace congestion.calculator.Repositories
{
    public interface ICityRepository
    {
        Task<CityEntity> GetCityByIdAsync(int id);
        Task<bool> IsExistAsync(int id);
        Task<IEnumerable<CityEntity>> GetAllAsync();
    }
}
