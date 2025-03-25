using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationService.Models;
using ReservationService.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            await _reservationRepository.AddReservationAsync(reservation);
            return CreatedAtAction(nameof(GetReservation), new { ReservationId = reservation.ReservationId }, reservation);
        }

        [HttpPut("{ReservationId}")]
        public async Task<IActionResult> PutReservation(int ReservationId, Reservation reservation)
        {
            if (ReservationId != reservation.ReservationId)
            {
                return BadRequest();
            }

            await _reservationRepository.UpdateReservationAsync(reservation);
            return NoContent();
        }

        [HttpDelete("{ReservationId}")]
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
