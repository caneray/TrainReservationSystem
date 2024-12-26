using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainReservationSystem.Application.Dto;
using TrainReservationSystem.Domain.Entities;

namespace TrainReservationSystem.Application.Interfaces
{
    public interface IWagonService
    {
        Task<Wagon> CreateWagonAsync(WagonDto wagonDto);
        Task<Wagon> GetWagonByIdAsync(Guid wagonId);
    }
}
