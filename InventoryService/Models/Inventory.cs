using System;
using System.Collections.Generic;

namespace InventoryService.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public string ItemName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public int DepartmentId { get; set; }
}
