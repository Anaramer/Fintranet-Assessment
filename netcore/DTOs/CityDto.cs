using System.ComponentModel.DataAnnotations;

namespace congestion.calculator.DTOs
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxTaxInOneDay { get; set; }
        [MinLength(1)]
        public int SingleChargePeriodMinute { get; set; }
    }
}
