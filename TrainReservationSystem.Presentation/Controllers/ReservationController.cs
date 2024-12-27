using Microsoft.AspNetCore.Mvc;
using TrainReservationSystem.Application.Dto;
using TrainReservationSystem.Application.Interfaces;

namespace TrainReservationSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationRequestDto requestDto)
        {
            if (requestDto == null)
                return BadRequest("Reservation data is required.");

            try
            {
                var reservation = await _reservationService.CreateReservationAsync(requestDto);
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation(Guid id)
        {
            var reservation = await _reservationService.GetReservationAsync(id);
            if (reservation == null)
                return NotFound($"Reservation with ID {id} not found.");

            return Ok(reservation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return Ok();
        }
    }
}
