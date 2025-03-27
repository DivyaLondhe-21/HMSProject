using EmployeeService.Interface;
using EmployeeService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private readonly EmployeeDbContext _context;
        private readonly DepartmentRepository _departmentRepository;
        public EmployeeRepository(EmployeeDbContext context, DepartmentRepository departmentRepository) 
        {
           _context = context;
           _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Staff>> GetAllStaff()
        {
            var allStaffs = await _context.Staffs.ToListAsync();
            return allStaffs; 
        }

        public async Task<Staff> GetStaffById(int staffID)
        {
            var staff = await _context.Staffs
                                           .FirstOrDefaultAsync(d => d.StaffId == staffID);

            if (staff == null)
            {
                throw new KeyNotFoundException("Department not found");
            }
            return staff;
        }

        public async Task<IEnumerable<Staff>> GetAllStaffofDepartment(string departmentName)
        {
            var departmentID = await _departmentRepository.GetDepartmentID(departmentName);
            if(departmentID == null)
            {
                throw new KeyNotFoundException("Department does not exist");
            }

            var staffs = await _context.Staffs.Where(s=> s.DepartmentId == departmentID).ToListAsync();
            return staffs;
        }
        public async Task<Staff> AddStaff(Staff staff)
        {
            await _context.Staffs.AddAsync(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<Staff> UpdateStaff(int staffID, Staff updatedStaff)
        {
            var existingStaff = await _context.Staffs.FindAsync(staffID);
            if (existingStaff == null)
            {
                throw new KeyNotFoundException("Department not found");
            }

            existingStaff.EmployeeName = updatedStaff.EmployeeName;
            existingStaff.Occupation = updatedStaff.Occupation;
            existingStaff.Salary = updatedStaff.Salary;
            existingStaff.NIC = updatedStaff.NIC;
            existingStaff.EmployeeAddress = updatedStaff.EmployeeAddress;
            existingStaff.Age  = updatedStaff.Age;
            existingStaff.Email = updatedStaff.Email;
            existingStaff.DepartmentId = updatedStaff.DepartmentId;
            

            _context.Staffs.Update(existingStaff);
            await _context.SaveChangesAsync();
            return updatedStaff;
        }
        public async Task DeleteStaff(int staffID)
        {
            var staff = await _context.Staffs.FindAsync(staffID);

            if (staff == null)
            {
                throw new KeyNotFoundException("Staff not found");
            }

            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
        }
    }
}
