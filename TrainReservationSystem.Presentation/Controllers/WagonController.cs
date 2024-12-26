using Microsoft.AspNetCore.Mvc;
using TrainReservationSystem.Application.Dto;
using TrainReservationSystem.Application.Interfaces;
using TrainReservationSystem.Domain.Entities;

namespace TrainReservationSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WagonController : ControllerBase
    {
        private readonly IWagonService _wagonService;
        public WagonController(IWagonService wagonService)
        {
            _wagonService = wagonService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWagon(WagonDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Wagon data is required");
            }
            var wagon = await _wagonService.CreateWagonAsync(dto);
            return Ok(wagon);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWagonById(Guid id)
        {
            var wagon = await _wagonService.GetWagonByIdAsync(id);
            if (wagon == null)
                return NotFound();

            return Ok(wagon);
        }
    }
}
