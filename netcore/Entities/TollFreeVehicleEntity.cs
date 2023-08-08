using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using congestion.calculator.Enums;

namespace congestion.calculator.Entities
{
    public class TollFreeVehicleEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public CityEntity City { get; set; }

        [Required]
        public VehicleEnum Vehicle { get; set; }
    }
}
