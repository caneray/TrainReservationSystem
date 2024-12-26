using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainReservationSystem.Application.Interfaces;
using TrainReservationSystem.Domain.Entities;
using TrainReservationSystem.Infrastructure;

namespace TrainReservationSystem.Application.Services
{
    public class SeatService : ISeatService
    {
        private readonly ApplicationDbContext _context;

        public SeatService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Seat>> GetSeatsByWagonIdAsync(Guid wagonId)
        {
            return await _context.Seats.Where(seat => seat.WagonId == wagonId).ToListAsync();
        }
    }
}
