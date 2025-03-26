using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ReservationService.Models;

namespace PaymentService.Models;

public partial class Payment
{
    [Key]
    public int PaymentId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Total must be a positive amount.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Payment time is required.")]
    public DateTime PayTime { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Credit Card Number is required.")]
    [RegularExpression(@"^\d{16}$", ErrorMessage = "Credit Card Number must be 16 digits.")]
    public string CreditCardNumber { get; set; }

    [Required(ErrorMessage = "Credit Card Type is required.")]
    [RegularExpression(@"^(Visa|MasterCard|American Express|Discover)$", ErrorMessage = "Credit Card Type must be one of the following: Visa, MasterCard, American Express, Discover.")]
    public string CreditCardType { get; set; }

    [Required]
    [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV must be 3 digits.")]
    public string Cvv { get; set; }

    [Required(ErrorMessage = "Card Holder Name is required.")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Card Holder Name must contain only letters and spaces.")]
    public string CardHolderName { get; set; }

    [Required(ErrorMessage = "Credit Card End Date is required.")]
    [DataType(DataType.Date)]
    public DateTime CreditExpiryDate { get; set; }

    [Required]
    public int ReservationId { get; set; }

    [ForeignKey("ReservationId")]
    public Reservation Reservation { get; set; }

    public ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
