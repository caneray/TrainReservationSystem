using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainReservationSystem.Application.Dto;
using TrainReservationSystem.Domain.Entities;

namespace TrainReservationSystem.Application.Interfaces
{
    public interface ITrainService
    {
        Task<Train> CreateTrainAsync(TrainDto trainDto);
        Task<Train> GetTrainByIdAsync(Guid trainId);
        Task<IEnumerable<Train>> GetAllTrainsAsync();
    }
}
