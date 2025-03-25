using System;
using System.Collections.Generic;

namespace PaymentService.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PayTime { get; set; }

    public string CreditCardNumber { get; set; } = null!;

    public string CreditCardType { get; set; } = null!;

    public string Cvv { get; set; } = null!;

    public string CardHolderName { get; set; } = null!;

    public DateTime CreditExpiryDate { get; set; }

    public int ReservationId { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
