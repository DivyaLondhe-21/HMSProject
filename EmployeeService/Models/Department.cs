using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Models;

public partial class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "Department name is required.")]
    [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
    public string DepartmentName { get; set; }

    public ICollection<Staff> Staffs { get; set; } = new List<Staff>();

}
