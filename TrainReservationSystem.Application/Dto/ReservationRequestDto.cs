using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainReservationSystem.Application.Dto
{
    public class ReservationRequestDto
    {
        public Guid TrainId { get; set; }
        public Guid UserId { get; set; }
        public List<PassengerDto> Passengers { get; set; } = new List<PassengerDto>();
    }
}
