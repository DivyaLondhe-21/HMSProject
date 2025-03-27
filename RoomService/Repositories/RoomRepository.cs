using Microsoft.EntityFrameworkCore;
using RoomService.Models;
using RoomService.Interface;

namespace RoomService.Repositories
{
    public class RoomRepository : IRoom
    {
        private readonly RoomDbContext _context;

        public RoomRepository(RoomDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms.Include(r => r.Rates).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync()
        {
            return await _context.Rooms.Where(r=> r.Availability== true).ToListAsync();
        }
        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _context.Rooms
                .Include(r => r.Rates)
                .FirstOrDefaultAsync(r => r.RoomID == id);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> UpdateRoomAsync(int id, Room room)
        {
            var existingRoom = await _context.Rooms.FindAsync(id);
            if (existingRoom == null)
            {
                return null;
            }

            existingRoom.RoomType = room.RoomType;
            existingRoom.Period = room.Period;
            existingRoom.CheckInDate = room.CheckInDate;
            existingRoom.CheckOutDate = room.CheckOutDate;
            existingRoom.Availability = room.Availability;

            await _context.SaveChangesAsync();
            return existingRoom;
        }

        public async Task<bool> UpdateAvailabilityOfRoom(int id, bool isAvailable)
        {
            var existingRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomID == id);
            if (existingRoom == null)
            {
                return false;
            }
            else
            {
                if (existingRoom.Availability)
                {
                    return true;
                }
                else
                {
                    existingRoom.Availability = isAvailable;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return false;
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
