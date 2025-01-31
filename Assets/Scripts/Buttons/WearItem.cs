using System;
using TMPro;
using UnityEngine;

public class WearItem : ItemButton
{
    private InventoryUI inventoryUI;

    private PlayerInventory playerInventory;
    private InventoryArmor inventoryArmor;

    [SerializeField] private TextMeshProUGUI textButton;
    private Cell putCell = null;
    private CellUI cellUI = null;

    private bool isWeared = false;

    public override void SetCell(CellUI cell)
    {
        base.SetCell(cell);
        inventoryUI = cell.getInventoryUI;
        cellUI = cell;

        playerInventory = FindAnyObjectByType<PlayerInventory>();
        inventoryArmor = FindAnyObjectByType<InventoryArmor>();

        CheckArmor();
    }
    protected override void AddEvent()
    {
        Debug.Log("add listener");
        button.onClick.AddListener(() => WearArmor());
    }

    private void CheckArmor()
    {
        foreach(Cell cell in inventoryArmor.getCells)
        {
            ItemData item = cell.item;

            if (cellUI.getCell.item.typesCell.Contains(cell.typeCell))
            {
                if (item == cellUI.getCell.item)
                {
                    textButton.text = "Снять";
                    isWeared = true;
                }
                else
                {
                    textButton.text = "Надеть";
                    isWeared = false;
                }

                break;
            }
        }
    }

    public void WearArmor()
    {
        if (isWeared)
        {
            putCell = CheckFreeCell(playerInventory);

            if (putCell != null)
            {
                Debug.Log(isWeared);

                DragDrop.instance.SetPutInventory(playerInventory);
                DragDrop.instance.SetPutCell(putCell.index);
            }
        }
        else
        {
            putCell = CheckArmorCell(inventoryArmor);

            DragDrop.instance.SetPutInventory(inventoryArmor);
            DragDrop.instance.SetPutCell(putCell.index);
        }

        DragDrop.instance.SetStartInventory(inventoryUI.getInventory);
        DragDrop.instance.SetStartCell(cell.getIndex);

        if (putCell == null)
        {
            DragDrop.instance.ResetData();
            return;
        }

        DragDrop.instance.TryPutCell();
        inventoryUI.CloseInfoPanel();
    }

    private Cell CheckFreeCell(EntityInventory inventory)
    {
        Cell returnCell = new Cell();

        Debug.Log("Invenoty count: " + inventory.getCells.Count);

        foreach (Cell cell in inventory.getCells)
        {
            Debug.Log(cell.count);
            Debug.Log(cell.index);
            if (cell.isTake == false)
            {
                returnCell = cell;
                break;
            }
        }

        return returnCell;
    }


    private Cell CheckArmorCell(EntityInventory inventory)
    {
        Cell returnCell = new Cell();

        Debug.Log("Armor count: " + inventory.getCells.Count);
        foreach (Cell cell in inventory.getCells)
        {
            Debug.Log(cell.count);
            Debug.Log(cell.index);
            if (cellUI.getCell.item.typesCell.Contains(cell.typeCell))
            {
                returnCell = cell;
                break;
            }
        }

        return returnCell;
    }
}
