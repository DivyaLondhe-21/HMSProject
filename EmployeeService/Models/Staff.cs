using System;
using System.Collections.Generic;

namespace EmployeeService.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public int DepartmentId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string EmployeeAddress { get; set; } = null!;

    public string Nic { get; set; } = null!;

    public decimal Salary { get; set; }

    public int Age { get; set; }

    public string Occupation { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string HiredBy { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;
}
