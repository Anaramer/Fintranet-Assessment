using congestion.calculator.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace congestion.calculator.Entities
{
    public class TollFreeDayOfWeekEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public CityEntity City { get; set; }

        [Required]
        public DayOfWeekEnum DayOfWeek { get; set; }
    }
}
