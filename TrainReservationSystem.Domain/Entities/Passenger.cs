using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainReservationSystem.Domain.Entities
{
    public class Passenger
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        // Bu yolcunun hangi rezervasyona ait olduğu
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
