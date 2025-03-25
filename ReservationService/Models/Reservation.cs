using System;
using System.Collections.Generic;

namespace ReservationService.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public int NumberOfChildren { get; set; }

    public int NumberOfAdults { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public int NumberOfNights { get; set; }

    public int RoomId { get; set; }

    public int? GuestId { get; set; }

    public virtual Guest? Guest { get; set; }
}
