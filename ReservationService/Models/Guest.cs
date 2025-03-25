using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReservationService.Models;

public partial class Guest
{

    [Key]
    public int GuestId { get; set; }

    [Required(ErrorMessage = "Please provide a phone number.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [RegularExpression(@"^\+91\s[7-9]\d{9}$", ErrorMessage = "Phone number must be in the format (+91) 98765 43210.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Please provide your company name.")]
    [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
    public string Company { get; set; }

    [Required(ErrorMessage = "Please provide your full name.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please provide your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please specify your gender.")]
    [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
    public string Gender { get; set; }

    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
    public string Address { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
