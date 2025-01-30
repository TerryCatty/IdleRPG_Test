using UnityEngine;

public class InfoProtect : InfoParameter
{
    public override void SetCell(Cell cell)
    {
        base.SetCell(cell);
        ItemArmor item = cell.item as ItemArmor;

        textValue.text = item.protect <= 0 ?
             item.protect.ToString() :
            "+" + item.protect;
    }
}
