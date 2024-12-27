using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainReservationSystem.Domain.Entities
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime? ReservationDate { get; set; }
        public Guid TrainId { get; set; }
        public Train? Train { get; set; }
        // Hangi vagon için rezervasyon yapıldı
        //public Guid WagonId { get; set; }
        //public Wagon? Wagon { get; set; }

        // Rezervasyonu yapan kullanıcı (owner)
        public Guid OwnerId { get; set; }
        public User? Owner { get; set; }

        // Navigation Property: Bu rezervasyonda kayıtlı yolcular
        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}
