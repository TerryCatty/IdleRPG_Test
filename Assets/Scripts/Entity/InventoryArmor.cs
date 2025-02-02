using System.Collections.Generic;
using UnityEngine;

public class InventoryArmor : EntityInventory
{

    public override void Init()
    {
        inventoryUI?.SetInventory(this);

        base.Init();

        if (inventoryUI == null) return;
        inventoryUI?.Init();

        for (int i = 0; i < cells.Count; i++)
        {
            inventoryUI?.SetCell(i, cells[i]);
        }
    }

    public override void SetCell(int index, Cell cell)
    {
        cells[index].item = cell.item;
        cells[index].count = cell.count;

        inventoryUI?.SetCell(index, cell);
    }
}
