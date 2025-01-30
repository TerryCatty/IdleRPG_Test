using TMPro;
using UnityEngine;

public class WearItem : ItemButton
{
    private InventoryUI inventoryUI;

    [SerializeField] private TextMeshProUGUI textButton;
    private CellUI cellUI = null;

    private bool isWeared = false;

    public override void SetCell(CellUI cell)
    {
        base.SetCell(cell);
        inventoryUI = cell.getInventoryUI;

        CheckArmor();
    }
    protected override void AddEvent()
    {
        Debug.Log("add listener");
        button.onClick.AddListener(() => WearArmor());
    }

    private void CheckArmor()
    {

        foreach (ArmorCellUI cell in inventoryUI.getCellsArmor)
        {
            ItemArmor item = this.cell.getCell.item as ItemArmor;
            if (cell.armorType == item.armorType)
            {
                cellUI = cell;

                if (cell == this.cell)
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
            cellUI = null;
            CheckFreeCell();
        }


        if (cellUI == null) return;

        inventoryUI.SetPutCell(cell.getIndex);
        inventoryUI.TryPutItemInCell(cellUI);
        inventoryUI.CloseInfoPanel();
    }

    private void CheckFreeCell()
    {
        foreach (CellUI cell in inventoryUI.getCells)
        {
            if (cell.getCell.isTake == false)
            {
                cellUI = cell;
                break;
            }
        }
    }
}
