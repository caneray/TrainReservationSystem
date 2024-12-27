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
            var train = await _context.Trains.FindAsync(requestDto.TrainId);
            var wagon = await _context.Wagons.FindAsync(requestDto.WagonId);
            if (wagon == null)
                throw new Exception("Wagon not found.");
            var seat = await _context.Seats.FirstOrDefaultAsync(r => r.WagonId == wagon.Id && r.SeatNumber == requestDto.SelectedSeatNumber);
            var ownerUser = await _context.Users.FindAsync(requestDto.UserId);
            if (ownerUser == null)
                throw new Exception("User (reservation owner) not found.");

            if (seat == null)
                throw new Exception("This seat does not exist");

            if (seat.IsReserved)
                throw new Exception($"Seat {seat.SeatNumber} is already reserved");

            seat.IsReserved = true;

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
                };
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
