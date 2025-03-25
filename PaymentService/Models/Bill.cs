using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models;

public partial class Bill
{
    [Key]
    public int BillId { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, 100, ErrorMessage = "Quantity must be at least 1.")]

    public int Quantity { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]

    public decimal Price { get; set; }

    [Required(ErrorMessage = "Taxes are required.")]
    [Range(0, 100, ErrorMessage = "Taxes cannot be negative.")]

    public decimal Taxes { get; set; }

    [Required(ErrorMessage = "Date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]

    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Service is required.")]
    [StringLength(100, ErrorMessage = "Service name cannot exceed 100 characters.")]

    public string Service { get; set; }

    [StringLength(50, ErrorMessage = "Unit name cannot exceed 50 characters.")]

    public string Unit { get; set; }

    [Required(ErrorMessage = "PaymentId is required.")]
    public int PaymentId { get; set; }

    [ForeignKey("PaymentId")]
    public Payment Payment { get; set; }
}
