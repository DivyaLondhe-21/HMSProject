using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationService.Models;
using ReservationService.ViewModel;

namespace ReservationService.Interface
{
    public interface IReservation
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int ReservationId);
        Task<IEnumerable<Reservation>> GetActiveReservationAsync();
        Task<Reservation> AddReservationAsync(ReservationViewModel reservation);
        Task<ReservationViewModel> UpdateReservationAsync(int reservationId, ReservationViewModel reservation);
        Task DeleteReservationAsync(int ReservationId);
    }
}
