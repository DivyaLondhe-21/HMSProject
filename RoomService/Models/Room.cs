using System;
using System.Collections.Generic;

namespace RoomService.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomType { get; set; } = null!;

    public decimal Price { get; set; }

    public string Period { get; set; } = null!;

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public bool Availability { get; set; }

    public int GuestId { get; set; }

    public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();
}
