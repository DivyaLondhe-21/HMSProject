using InventoryService.Models;

namespace InventoryService.Repositories
{
    public class DepartmentRepository
    {
        private readonly InventoryDbContext _context;
        public DepartmentRepository(InventoryDbContext context) 
        {
            _context = context;
        }


        public async Task<int> GetDepartmentID(string DepartmentName)
        {
            var departmentID = await _context.Departments.FindAsync(DepartmentName);
            return departmentID.DepartmentId;
        }
    }
}
