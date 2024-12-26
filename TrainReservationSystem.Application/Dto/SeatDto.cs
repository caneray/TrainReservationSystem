using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainReservationSystem.Application.Dto
{
    public class SeatDto
    {
        public string SeatNumber { get; set; }
        public bool IsReserved { get; set; }
    }
}
