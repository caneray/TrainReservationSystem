using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainReservationSystem.Application.Dto;
using TrainReservationSystem.Application.Interfaces;
using TrainReservationSystem.Domain.Entities;
using TrainReservationSystem.Infrastructure;

namespace TrainReservationSystem.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> CreateReservationAsync(ReservationRequestDto requestDto)
        {

            var train = await _context.Trains.Include(x => x.Wagons).ThenInclude(x => x.Seats).FirstOrDefaultAsync(x => x.Id == requestDto.TrainId);

            var ownerUser = await _context.Users.FindAsync(requestDto.UserId);
            if (ownerUser == null)
                throw new Exception("User (reservation owner) not found.");

            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                TrainId = train.Id,
                OwnerId = ownerUser.Id,
                ReservationDate = DateTime.UtcNow
            };

            foreach (var passengerDto in requestDto.Passengers)
            {
                var passengerEntity = new Passenger
                {
                    Id = Guid.NewGuid(),
                    FullName = passengerDto.FullName,
                    ReservationId = reservation.Id,
                    SelectedSeatNumber = passengerDto.SelectedSeatNumber,
                    
                };

                foreach (var wagon in train.Wagons)
                {
                    foreach (var seat in wagon.Seats)
                    {
                        if (seat.SeatNumber == passengerDto.SelectedSeatNumber && seat.IsReserved == true && seat.WagonId == passengerDto.WagonId)
                        {
                            throw new Exception("The seat that you have select is taken: " + seat.SeatNumber);
                        }
                        if (seat.SeatNumber == passengerDto.SelectedSeatNumber && seat.WagonId == passengerDto.WagonId)
                        {
                            seat.IsReserved = true;
                        }
                    }
                }
                reservation.Passengers.Add(passengerEntity);

            }
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return reservation; 
        }

        public async Task DeleteReservationAsync(Guid reservationId)
        {
            var reservation = await _context.Reservations.Include(x => x.Train).ThenInclude(x => x.Wagons).ThenInclude(x => x.Seats).FirstOrDefaultAsync(x => x.Id == reservationId);

            if (reservation == null)
            {
                throw new Exception("Reservation not found.");
            }
            foreach (var wagon in reservation.Train.Wagons)
            {
                foreach (var seat in wagon.Seats)
                {
                    if (seat.IsReserved)
                    {
                        seat.IsReserved = false;
                    }
                }
            }
            await Task.Run(()=> _context.Remove(reservation));
            await _context.SaveChangesAsync();

        }

        public async Task<Reservation> GetReservationAsync(Guid reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Passengers)
                .Include(r => r.Train)
                .Include(r => r.Owner)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            return reservation;
        }
    }
}
