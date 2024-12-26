using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using TrainReservationSystem.Application.Dto;
using TrainReservationSystem.Application.Interfaces;

namespace TrainReservationSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainController : ControllerBase
    {
        private readonly ITrainService _trainService;
        public TrainController(ITrainService trainService)
        {
            _trainService = trainService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrain(TrainDto dto)
        {
            var train = await _trainService.CreateTrainAsync(dto);
            return Ok(train);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainById(Guid id)
        {
            var train = await _trainService.GetTrainByIdAsync(id);
            if (train == null)
                return NotFound();

            return Ok(train);
        }
    }
}
