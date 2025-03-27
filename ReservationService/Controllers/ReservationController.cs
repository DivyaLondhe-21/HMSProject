using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationService.Models;
using ReservationService.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationService.ViewModel;

namespace ReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservation _reservationRepository;

        public ReservationController(IReservation reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            var reservations = await _reservationRepository.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{ReservationId}")]
        public async Task<ActionResult<Reservation>> GetReservation(int ReservationId)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(ReservationId);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetActiveReservation()
        {
            var reservations = await _reservationRepository.GetActiveReservationAsync();
            return Ok(reservations);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Reservation>> CreateReservation(ReservationViewModel reservation)
        {
    
           var createdReservation =  await _reservationRepository.AddReservationAsync(reservation);

            return CreatedAtAction(nameof(GetReservation), new { ReservationId = createdReservation.ReservationId }, reservation);
        }

        [HttpPut("update/{ReservationId}")]
        public async Task<IActionResult> PutReservation(int ReservationId, ReservationViewModel reservation)
        {
            if (ReservationId<=0)
            {
                return BadRequest("Invalid Reservation ID");
            }
            var existingReservation = await _reservationRepository.GetReservationByIdAsync(ReservationId);
            if (existingReservation == null)
            {
                return NotFound("Reservation not found");
            }

            await _reservationRepository.UpdateReservationAsync(ReservationId, reservation);
            return NoContent();
        }

        [HttpDelete("remove/{ReservationId}")]
        public async Task<IActionResult> DeleteReservation(int ReservationId)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(ReservationId);
            if (reservation == null)
            {
                return NotFound();
            }

            await _reservationRepository.DeleteReservationAsync(ReservationId);
            return NoContent();
        }
    }
}
