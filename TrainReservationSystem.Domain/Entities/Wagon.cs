using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainReservationSystem.Domain.Entities
{
    public class Wagon
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } 
        public Guid TrainId { get; set; }
        public Train Train { get; set; }
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();

        //Kapasitesi eklenmeli.
    }
}
