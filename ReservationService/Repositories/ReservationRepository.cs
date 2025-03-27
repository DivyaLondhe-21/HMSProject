using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationService.Models;
using ReservationService.Interface;
using ReservationService.ViewModel;

namespace ReservationService.Repositories
{
    public class ReservationRepository : IReservation
    {
        private readonly ReservationDbContext _context;
        private readonly RoomRepository _roomRepository;

        public ReservationRepository(ReservationDbContext context, RoomRepository roomRepository)
        {
            _context = context;
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations.Include(r=> r.Room).ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int ReservationId)
        {
            return await _context.Reservations.Include(r=> r.Room).FirstOrDefaultAsync(r=> r.ReservationId==ReservationId) ?? throw new KeyNotFoundException("Reservation not found");
        }

        public async Task<IEnumerable<Reservation>> GetActiveReservationAsync()
        {
            return await _context.Reservations.Include(r => r.Room).Where(r => r.CheckInDate == DateTime.Today).ToListAsync();
        }
        public async Task<Reservation> AddReservationAsync(ReservationViewModel reservation)
        {
            int numberOfDays = reservation.CalculateNumOfNights();

            var rate = _roomRepository.GetRoomByIdAsync(reservation.RoomId);
            reservation.Price = reservation.CalculatePrice(rate, numberOfDays);

            var guestInfo = new Guest
            {
                Name = reservation.Name,
                PhoneNumber = reservation.PhoneNumber,
                Address = reservation.Address,
                Company = reservation.Company,
                Email = reservation.Email,
                Gender = reservation.Gender
            };
            await _context.Guests.AddAsync(guestInfo);

            var guestID = await _context.Guests.FirstOrDefaultAsync(g => g.Email ==  reservation.Email);
            var reservationEntity = new Reservation
            {
                NumberOfAdults = reservation.NumberOfAdults,
                NumberOfChildren = reservation.NumberOfChildren,
                NumberOfNights = numberOfDays,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                RoomId = reservation.RoomId,
                GuestId = guestID.GuestId

            };

            await _context.Reservations.AddAsync(reservationEntity);

            var roomUpdates = new Room
            {
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                GuestId = guestID.GuestId,
                Availability = false,
                Period = numberOfDays.ToString(),
              
            };
            await _context.Rooms.AddAsync(roomUpdates);
            await _context.SaveChangesAsync();
            return reservationEntity;
        }

        /*        public async Task UpdateReservationAsync(Reservation reservation)
                {
                    _context.Reservations.Update(reservation);
                    await _context.SaveChangesAsync();
                }*/

        public async Task<ReservationViewModel> UpdateReservationAsync(int reservationId, ReservationViewModel updatedReservation)
        {

            var reservationEntity = await _context.Reservations
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            if (reservationEntity == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }


            reservationEntity.NumberOfAdults = updatedReservation.NumberOfAdults;
            reservationEntity.NumberOfChildren = updatedReservation.NumberOfChildren;
            reservationEntity.CheckInDate = updatedReservation.CheckInDate;
            reservationEntity.CheckOutDate = updatedReservation.CheckOutDate;


            int numberOfDays = updatedReservation.CalculateNumOfNights();
            reservationEntity.NumberOfNights = numberOfDays;

            var rate =  _roomRepository.GetRoomByIdAsync(updatedReservation.RoomId);
            updatedReservation.Price = updatedReservation.CalculatePrice(rate, numberOfDays);


            var guest = await _context.Guests
                .FirstOrDefaultAsync(g => g.Email == updatedReservation.Email);

            if (guest != null)
            {
                guest.Name = updatedReservation.Name;
                guest.PhoneNumber = updatedReservation.PhoneNumber;
                guest.Address = updatedReservation.Address;
                guest.Company = updatedReservation.Company;
                guest.Gender = updatedReservation.Gender;
            }
            else
            {
                throw new KeyNotFoundException("Guest not found for the provided email.");
            }

    
            var roomEntity = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomID == updatedReservation.RoomId);
            if (roomEntity != null)
            {
                roomEntity.CheckInDate = updatedReservation.CheckInDate;
                roomEntity.CheckOutDate = updatedReservation.CheckOutDate;
                roomEntity.GuestId = guest.GuestId;
                roomEntity.Availability = false;
                roomEntity.Period = numberOfDays.ToString();
            }
            else
            {
                throw new KeyNotFoundException("Room not found.");
            }


            await _context.SaveChangesAsync();

            return updatedReservation;
        }


        public async Task DeleteReservationAsync(int ReservationId)
        {
            var reservation = await _context.Reservations.FindAsync(ReservationId);
            if (reservation != null)
            {
                var roomEntity = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomID == reservation.RoomId);
                if (roomEntity != null)
                {
                     roomEntity.Availability = true;

                }
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
