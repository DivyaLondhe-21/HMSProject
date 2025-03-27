using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Models;

public partial class Staff
{
    [Key]
    public int StaffId { get; set; }

    public int DepartmentId { get; set; }
    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }

    [Required(ErrorMessage = "Employee name is required.")]
    [StringLength(100, ErrorMessage = "Employee name cannot exceed 100 characters.")]
    public string EmployeeName { get; set; }

    [Required(ErrorMessage = "Employee address is required.")]
    [StringLength(200, ErrorMessage = "Employee address cannot exceed 200 characters.")]
    public string EmployeeAddress { get; set; }

    [Required(ErrorMessage = "NIC is required.")]
    [StringLength(15, MinimumLength = 10, ErrorMessage = "NIC should be between 10 and 15 characters.")]
    public string NIC { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
    public decimal Salary { get; set; }

    [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Occupation is required.")]
    [StringLength(50, ErrorMessage = "Occupation cannot exceed 50 characters.")]
    public string Occupation { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    public string Email { get; set; }

 
    public string HiredBy { get; set; }
}
