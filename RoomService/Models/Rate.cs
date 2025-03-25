using System;
using System.Collections.Generic;

namespace RoomService.Models;

public partial class Rate
{
    public int RateId { get; set; }

    public decimal FirstNightPrice { get; set; }

    public decimal ExtensionPrice { get; set; }

    public int NumberOfChildren { get; set; }

    public int NumberOfGuests { get; set; }

    public int NumberOfDays { get; set; }

    public int RoomId { get; set; }

    public virtual Room Room { get; set; } = null!;
}
