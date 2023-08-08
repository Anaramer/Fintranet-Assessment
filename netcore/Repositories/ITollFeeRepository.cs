using congestion.calculator.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace congestion.calculator.Repositories
{
    public interface ITollFeeRepository
    {
        Task<IReadOnlyList<TollFeeEntity>> GetAllByCityIdAsync(int cityId);
    }
}
