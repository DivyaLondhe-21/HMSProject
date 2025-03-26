using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReservationService.Models;

public partial class Rate
{
    [Key]
    public int RateId { get; set; }

    [Required]
    [Range(1000, double.MaxValue, ErrorMessage = "Price must be greater than 1000.")]
    public decimal FirstNightPrice { get; set; }

    [Required]
    [Range(500, double.MaxValue, ErrorMessage = "Price must be greater than 500.")]
    public decimal ExtensionPrice { get; set; }

    [Required]
    public int NumberOfChildren { get; set; }

    [Required]
    [Range(1, 3, ErrorMessage = "Number of guests must be between 1 to 3.")]
    public int NumberOfGuests { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Number of days must be atleast 1.")]
    public int NumberOfDays { get; set; }

    //[Required]
    //public int Discount { get; set; }

    [Required]
    public int RoomId { get; set; }
    [ForeignKey("RoomId")]
    public Room Room { get; set; }
}
