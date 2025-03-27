using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeService.Interface;
using EmployeeService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeRepository;
        private readonly IDepartment _departmentRepository;

        public EmployeeController(IEmployee employeeRepository, IDepartment departmentRepository)  
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpGet("departments")]
        [Authorize(Roles = "Admin,Manager,Receptionist")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentRepository.GetAllDepartments();
            return Ok(departments);
        }

        [HttpGet("staff")]
        [Authorize(Roles = "Admin,Manager,Receptionist")]
        public async Task<IActionResult> GetStaff()
        {
            var staff = await _employeeRepository.GetAllStaff();
            return Ok(staff);
        }

        [HttpGet("staff/{departmentName}")]
        [Authorize(Roles = "Admin,Manager,Receptionist")]
        public async Task<IActionResult> GetStaffOfaDepartment(string departmentName)
        {
            var staff = await _employeeRepository.GetAllStaffofDepartment(departmentName);
            if (staff.IsNullOrEmpty())
            {
                return BadRequest("Staff not found");
            }
            return Ok(staff);
        }

        [HttpGet("staff/{staffID}")]
        [Authorize(Roles = "Admin,Manager,Receptionist")]
        public async Task<IActionResult> GetStaffbyId(int staffID)
        {
            var staff = await _employeeRepository.GetStaffById(staffID);
            if (staff == null)
            {
                return BadRequest("Staff not found");
            }
            return Ok(staff);
        }

        [HttpPost("staff")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AddStaff(Staff staff)
        {
            await _employeeRepository.AddStaff(staff);
            return Ok("Staff created successfully.");
        }

        [HttpPut("staff/{staffId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateStaff(int staffId, Staff staff)
        {
            await _employeeRepository.UpdateStaff(staffId, staff);
            return Ok("Staff updated successfully.");
        }

        [HttpDelete("staff/{staffId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteStaff(int staffId)
        {
            await _employeeRepository.DeleteStaff(staffId);
            return Ok("Staff deleted successfully.");
        }

        [HttpPost("departments")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            await _departmentRepository.AddDepartment(department);
            return Ok("Department created successfully.");
        }

        [HttpPut("departments/{departmentId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDepartment(int departmentId, Department department)
        {
            await _departmentRepository.UpdateDepartment(departmentId, department);
            return Ok("Department updated successfully.");
        }

        [HttpDelete("departments/{departmentId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveDepartment(int departmentId)
        {
            await _departmentRepository.RemoveDepartment(departmentId);
            return Ok("Department deleted successfully.");
        }
    }
}

