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
            //var train = await _context.Trains.FindAsync(requestDto.TrainId);
            //if (train == null)
            //{
            //    throw new Exception("Train Not Found");
            //}
            //var wagon = await _context.Wagons.FindAsync(requestDto.WagonId);
            //if (wagon == null)
            //{
            //    throw new Exception("Wagon Not Found");
            //}
            //var ownerUser = await _context.Users.FindAsync(requestDto.WagonId);
            //if (ownerUser == null)
            //{
            //    throw new Exception("User Not Found");
            //}

            //var reservation = new Reservation
            //{
            //    Id = Guid.NewGuid(),
            //    WagonId = wagon.Id,
            //    OwnerId = ownerUser.Id,
            //    ReservationDate = DateTime.Now,
            //};

            //foreach (var passengerDto in requestDto.Passengers)
            //{
            //    var passengerEntity = new Passenger
            //    {
            //        Id = Guid.NewGuid(),
            //        FullName = passengerDto.FullName,
            //        ReservationId = reservation.Id
            //    };
            //    reservation.Passengers.Add(passengerEntity);
            //}

            //_context.Reservations.Add(reservation);
            //await _context.SaveChangesAsync();

            //return reservation;


            var train = await _context.Trains.FindAsync(requestDto.TrainId);
            if (train == null) throw new Exception("Train not found.");

            var wagon = await _context.Wagons.FindAsync(requestDto.WagonId);
            if (wagon == null) throw new Exception("Wagon not found.");

            // 2) Kullanıcının seçtiği koltuk var mı ve boş mu?
            var seat = await _context.Seats
                .Where(s => s.WagonId == wagon.Id && s.SeatNumber == requestDto.SelectedSeatNumber)
                .FirstOrDefaultAsync();

            if (seat == null)
                throw new Exception($"Seat {requestDto.SelectedSeatNumber} not found in wagon {wagon.Name}.");

            if (seat.IsReserved)
                throw new Exception($"Seat {seat.SeatNumber} is already reserved.");

            // 3) Rezervasyon kaydı oluştur
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                WagonId = wagon.Id,
                OwnerId = requestDto.UserId,
                ReservationDate = DateTime.UtcNow
            };


            seat.IsReserved = true; 
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        public async Task<Reservation> GetReservationAsync(Guid reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Passengers)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            return reservation;
        }
    }
}
