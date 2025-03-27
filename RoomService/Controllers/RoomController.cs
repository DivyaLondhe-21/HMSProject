using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomService.Models;
using RoomService.Interface;

[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly IRoom _roomRepository;

    public RoomController(IRoom roomRepository)
    {
        _roomRepository = roomRepository;
    }

    // GET: api/Room
    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        var rooms = await _roomRepository.GetAllRoomsAsync();
        return Ok(rooms);
    }
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableRooms()
    {
        var rooms = await _roomRepository.GetAvailableRoomsAsync();
        return Ok(rooms);
    }

    // GET: api/Room/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoom(int id)
    {
        var room = await _roomRepository.GetRoomByIdAsync(id);
        if (room == null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    // POST: api/Room
    [HttpPost("create")]
    public async Task<IActionResult> CreateRoom([FromBody] Room room)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdRoom = await _roomRepository.CreateRoomAsync(room);
        return CreatedAtAction(nameof(GetRoom), new { id = createdRoom.RoomID }, createdRoom);
    }

    // PUT: api/Room/5
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room room)
    {
        if (id != room.RoomID)
        {
            return BadRequest();
        }

        var updatedRoom = await _roomRepository.UpdateRoomAsync(id, room);
        if (updatedRoom == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("update/availability/{id}")]
    public async Task<IActionResult> UpdateRoomAvailability(int id, [FromBody] bool isAvailable)
    {
        var updatedRoom = await _roomRepository.UpdateAvailabilityOfRoom(id, isAvailable);
        if (updatedRoom == null)
        {
            return NotFound();
        }
        return Ok(updatedRoom);
    }

    // DELETE: api/Room/5
    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var deleted = await _roomRepository.DeleteRoomAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
