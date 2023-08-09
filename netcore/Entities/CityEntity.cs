using System.ComponentModel.DataAnnotations;

namespace congestion.calculator.Entities
{
    public class CityEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int MaxTaxInOneDay { get; set; }

        [MinLength(1)]
        public int SingleChargePeriodMinute { get; set; }
    }
}
