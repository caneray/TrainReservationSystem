using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainReservationSystem.Domain.Entities
{
    public class Seat
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string SeatNumber { get; set; }

        [Required]
        public bool IsReserved { get; set; } 

        public Guid WagonId { get; set; } 

        public Wagon Wagon { get; set; }
    }
}
