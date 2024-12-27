using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainReservationSystem.Application.Dto;
using TrainReservationSystem.Application.Interfaces;
using TrainReservationSystem.Domain.Entities;
using TrainReservationSystem.Infrastructure;

namespace TrainReservationSystem.Application.Services
{
    public class WagonService : IWagonService
    {
        private readonly ApplicationDbContext _context;
        public WagonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Wagon> CreateWagonAsync(WagonDto wagonDto)
        {
            var wagon = new Wagon
            {
                Id = Guid.NewGuid(),
                Name = wagonDto.Name,
                TrainId = wagonDto.TrainId
            };

            _context.Wagons.Add(wagon);

            var seatNumbers = new[] { "1A", "2A", "3A", "1B", "2B", "3B" };
            foreach (var seatNum in seatNumbers)
            {
                var seat = new Seat
                {
                    Id = Guid.NewGuid(),
                    SeatNumber = seatNum,
                    IsReserved = false,
                    WagonId = wagon.Id
                };
                _context.Seats.Add(seat);
            }
            await _context.SaveChangesAsync();

            return wagon;
        }
        public async Task<Wagon> GetWagonByIdAsync(Guid wagonId)
        {
            return await _context.Wagons.FindAsync(wagonId);
        }
    }
}
