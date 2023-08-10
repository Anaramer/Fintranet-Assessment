using System.ComponentModel.DataAnnotations;

namespace congestion.calculator.DTOs
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxTaxInOneDay { get; set; }
        public int SingleChargePeriodMinute { get; set; }
    }
}
