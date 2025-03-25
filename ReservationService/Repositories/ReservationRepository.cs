using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationService.Models;
using ReservationService.Interface;

namespace ReservationService.Repositories
{
    public class ReservationRepository : IReservation
    {
        private readonly ReservationDbContext _context;

        public ReservationRepository(ReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int ReservationId)
        {
            return await _context.Reservations.FindAsync(ReservationId) ?? throw new KeyNotFoundException("Reservation not found");
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(int ReservationId)
        {
            var reservation = await _context.Reservations.FindAsync(ReservationId);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
