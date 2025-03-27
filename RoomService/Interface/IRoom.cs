using RoomService.Models;
namespace RoomService.Interface

{
    public interface IRoom
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
        Task<Room> GetRoomByIdAsync(int id);
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(int id, Room room);
        Task<bool> UpdateAvailabilityOfRoom(int id, bool isAvailable);
        Task<bool> DeleteRoomAsync(int id);
    }

}
