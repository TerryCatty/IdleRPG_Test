using System;
using UnityEngine;

public class ArmorCellUI : CellUI
{
    public TypeArmor armorType;

    public override void SetCell(Cell cell)
    {
        
        this.cell.item = cell.item;
        this.cell.count = cell.count;


        UpdateText();
        UpdateImage();
    }

    public override void SetIndex(int value)
    {
        base.SetIndex(value);
        CellArmor myCell = inventoryUI.GetCell(value) as CellArmor;
        armorType = myCell.armorType;
    }

    public override bool TryGetNewItem(Cell cell)
    {
        bool result = false;


        if (cell.item as ItemArmor)
        {
            ItemArmor item = cell.item as ItemArmor;
            if (armorType ==
                item.armorType) result = true;
        }

        if (result == false)
            result = cell.count == 0;

        return result;
    }
}

[Serializable]

public enum TypeArmor
{
    Body,
    Head
}
