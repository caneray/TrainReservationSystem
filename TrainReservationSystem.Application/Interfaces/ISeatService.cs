using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainReservationSystem.Domain.Entities;

namespace TrainReservationSystem.Application.Interfaces
{
    public interface ISeatService
    {
        Task<List<Seat>> GetSeatsByWagonIdAsync(Guid wagonId);
    }
}
