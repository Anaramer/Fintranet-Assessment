using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using congestion.calculator.Enums;

namespace congestion.calculator.Repositories
{
    public interface ITollFreeMonthRepository
    {
        Task<bool> IsExistAsync(int cityId, MonthEnum month);
    }
}
