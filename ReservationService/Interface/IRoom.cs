using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationService.Models;
using ReservationService.ViewModel;

namespace ReservationService.Interface
{
    public interface IRoom
    {
      //  Task<IEnumerable<Room>> GetRoomRatesAsync();
        decimal GetRoomByIdAsync(int RoomId);

/*        Task<ReservationViewModel> AddReservationAsync(ReservationViewModel reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int ReservationId);*/
    }
}
