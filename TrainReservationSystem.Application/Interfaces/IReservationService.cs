using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainReservationSystem.Application.Dto;
using TrainReservationSystem.Domain.Entities;

namespace TrainReservationSystem.Application.Interfaces
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(ReservationRequestDto requestDto);
        Task<Reservation> GetReservationAsync (Guid reservationId);

    }
}
