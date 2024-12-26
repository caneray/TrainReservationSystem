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
    public class TrainService : ITrainService
    {
        private readonly ApplicationDbContext _context;
        public TrainService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Train> CreateTrainAsync(TrainDto trainDto)
        {
            var train = new Train
            {
                Id = Guid.NewGuid(),
                Name = trainDto.Name
            };

            _context.Trains.Add(train);
            await _context.SaveChangesAsync();
            return train;
        }

        public async Task<Train> GetTrainByIdAsync(Guid trainId)
        {
            var train = await _context.Trains
                .Include(t => t.Wagons)
                .FirstOrDefaultAsync(t => t.Id == trainId);



            return train;
        }

        public async Task<IEnumerable<Train>> GetAllTrainsAsync()
        {
            return await _context.Trains
                .Include(t => t.Wagons)
                .ToListAsync();
        }

    }
}
