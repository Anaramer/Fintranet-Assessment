using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace congestion.calculator.Entities
{
    public class TollFeeEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual CityEntity City { get; set; }

        /// <summary>
        /// Value as Minute
        /// ex : Clock 6:30 =&gt; (6 * 60) + 30 = 390
        /// </summary>
        public int StartTimeOfDay  { get; set; } 
        public int Fee { get; set; }
    }
}
