using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationService.Models;

namespace ReservationService.Interface
{
    public interface IReservation
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int ReservationId);
        Task AddReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int ReservationId);
    }
}
