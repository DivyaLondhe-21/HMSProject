using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryService.Interface;
using InventoryService.Repositories;
using Microsoft.AspNetCore.Authorization;
using InventoryService.Models;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventory _inventoryRepository;
        public InventoryController(IInventory inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can view
        public async Task<IActionResult> GetAllInventoryReq()
        {
            var inventoryItems = await _inventoryRepository.GetAllInventoryRequest();
            if (inventoryItems == null)
            {
                return NotFound("No Inventory Request Created");
            }
            return Ok(inventoryItems);
        }

        [HttpGet("inventory/{id}")]
        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can view
        public async Task<IActionResult> GetInventoryReqofDepartment(string departmentName)
        {
            var item = await _inventoryRepository.GetInventoryReqofDepartment(departmentName);
            if (item == null)
            {
                return NotFound("Item not found.");
            }
            return Ok(item);
        }

        [HttpGet("inventory/{itemName}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetItembyName(string itemName)
        {
            var inventoryItem = await _inventoryRepository.GetInventorybyItem(itemName);
            if (inventoryItem == null)
            {
                return NotFound("Inventory not found");
            }
            return Ok(inventoryItem);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can create
        public async Task<IActionResult> AddInventoryItem(Inventory inventory)
        {
            var newItem = await _inventoryRepository.CreateInventoryRequest(inventory);
            return CreatedAtAction(nameof(GetItembyName), new { id = newItem.InventoryId }, newItem);
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can update
        public async Task<IActionResult> UpdateInventoryItem(int id, Inventory updatedInventory)
        {
            if (id != updatedInventory.InventoryId)
            {
                return BadRequest("Inventory ID mismatch.");
            }

            var item = await _inventoryRepository.GetInventorybyItemID(id);
            if (item == null)
            {
                return NotFound("Item not found.");
            }

            var inventory = await _inventoryRepository.UpdateInventoryRequest(id, updatedInventory);
            return Ok(inventory);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin can delete
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var item = await _inventoryRepository.GetInventorybyItemID(id);
            if (item == null)
            {
                return NotFound("Item not found.");
            }

            await _inventoryRepository.RemoveInventoryRequest(id);
            return NoContent();
        }

    }
}
