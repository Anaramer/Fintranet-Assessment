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
        public DateTime HolidayDate { get; set; }
    }
}
