using System;
using System.Threading.Tasks;

namespace congestion.calculator.Repositories
{
    public interface ITollFreeDateRepository
    {
        Task<bool> IsExistAsync(DateTime date);
    }
}
