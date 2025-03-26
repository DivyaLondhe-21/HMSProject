using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationService.Interface;

namespace ReservationService.Controllers
{
    public class RoomController : Controller
    {
        private readonly IReservation _reservationRepository;

        public RoomController(IReservation reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("{roomId}/rate")]
        public async Task<IActionResult> GetRoomRate(int roomId)
        {
           // var room = await _reservationRepository.Rooms.FindAsync(roomId);

           /* if (room == null)
            {
                return NotFound("Room not found.");
            }*/

            return Ok();
        }
    }
}
