using System;
using System.Collections.Generic;

namespace PaymentService.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Taxes { get; set; }

    public DateTime Date { get; set; }

    public string Service { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public int PaymentId { get; set; }

    public virtual Payment Payment { get; set; } = null!;
}
