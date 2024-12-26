using Microsoft.AspNetCore.Mvc;
using TrainReservationSystem.Application.Interfaces;

namespace TrainReservationSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatController : Controller
    {
        private readonly ISeatService _seatService;
        public SeatController(ISeatService seatService)
        {
            _seatService = seatService;
        }
        [HttpGet("bywagon/{wagonId}")]
        public async Task<IActionResult> GetSeatsByWagon(Guid wagonId)
        {
            var seats = await _seatService.GetSeatsByWagonIdAsync(wagonId);
            if (seats == null || !seats.Any())
            {
                return NotFound($"No seats found for wagon {wagonId}");
            }

            return Ok(seats);
        }
    }
}
