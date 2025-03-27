using InventoryService.Interface;
using InventoryService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;

namespace InventoryService.Repositories
{
    public class InventoryRepository: IInventory
    {
        private readonly InventoryDbContext _context;
        private readonly DepartmentRepository _departmentRepository;

        public InventoryRepository(InventoryDbContext context, DepartmentRepository departmentRepository)
        {
            _context = context;
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoryRequest()
        {
            var inventoryList = await _context.Inventories.Include(i => i.Department).ToListAsync();
            return inventoryList;
        }

        public async Task<IEnumerable<Inventory>> GetInventoryReqofDepartment(string departmentName)
        {
            var departmentID = await _departmentRepository.GetDepartmentID(departmentName);
            var inventory = await _context.Inventories.Where(i => i.DepartmentId == departmentID).ToListAsync();
            return inventory;
        }

        public async Task<IEnumerable<Inventory>> GetInventorybyItem(string itemsName)
        {
            var inventory = await _context.Inventories.Include(i=> i.Department).Where(i => i.ItemName == itemsName).ToListAsync();
            return inventory;
        }
        public async Task<Inventory> GetInventorybyItemID(int itemsId)
        {
            var inventory = await _context.Inventories.FindAsync(itemsId);
            return inventory;
        }

        public async Task<Inventory> CreateInventoryRequest(Inventory inventory)
        {
            await _context.Inventories.AddAsync(inventory);
            await _context.SaveChangesAsync();
            return inventory;

        }

        public async Task<Inventory> UpdateInventoryRequest(int id, Inventory updatedInventory)
        {

            var existingItem = await _context.Inventories.FindAsync(id);
            if (existingItem != null)
            {
                existingItem.ItemName = updatedInventory.ItemName;
                existingItem.Quantity = updatedInventory.Quantity;
                existingItem.Price = updatedInventory.Price;
                existingItem.DepartmentId = updatedInventory.DepartmentId;

                _context.Inventories.Update(existingItem);
                await _context.SaveChangesAsync();
            }
            return existingItem;
        }

        public async Task RemoveInventoryRequest(int id)
        {
            var inventoryItem = await _context.Inventories.FindAsync(id);
            if (inventoryItem != null)
            {
                _context.Inventories.Remove(inventoryItem);
                await _context.SaveChangesAsync();
            }
        }



    }
}
