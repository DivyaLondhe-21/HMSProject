using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationService.Interface;

namespace ReservationService.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoom _roomRepository;

        public RoomController(IRoom roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
       /* [HttpGet("{roomId}/rate")]
        public async Task<IActionResult> GetRoomRate(int roomId)
        {
           *//*var room = await _roomRepository.GetRoomRates(roomId);

            if (room == null)
            {
                return NotFound("Room not found.");
            }

            return Ok(room);*//*
        }*/
    }
}
