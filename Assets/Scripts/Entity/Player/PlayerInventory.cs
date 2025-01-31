using UnityEngine;

public class PlayerInventory : Inventory
{

    public bool autoLoad;
    public override void Init()
    {
        filePath = Application.persistentDataPath + "/Cells/";
        inventoryUI?.SetInventory(this);

        LoadData();
        base.Init();

        InitUI();
    }

    public override void LoadData()
    {

        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].index = i;
        }
    }

    public override void SetCell(int index, Cell cell)
    {
        base.SetCell(index, cell);
        inventoryUI?.SetCell(index, cell);
    }


    public void InitUI()
    {
        if (inventoryUI == null) return;

        inventoryUI?.LoadCells(capacity);
        inventoryUI?.Init();

        for (int i = 0; i < cells.Count; i++)
        {
            inventoryUI?.SetCell(i, cells[i]);
        }

    }

    public override void DeleteItem(int indexCell)
    {
        base.DeleteItem(indexCell);

        inventoryUI?.SetCell(indexCell, cells[indexCell]);
    }
}
