using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace congestion.calculator.Entities
{
    public class TollFreeDateEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual CityEntity City { get; set; }

        public DateTime HolidayDate { get; set; }
    }
}
