using System;
using System.Threading.Tasks;

namespace congestion.calculator.Repositories
{
    public interface ITollFreeDayOfWeekRepository
    {
        Task<bool> IsExistAsync(int cityId, DayOfWeek dayOfWeek);
    }
}
