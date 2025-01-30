using UnityEngine;

public class InfoWeight : InfoParameter
{
    public override void SetCell(Cell cell)
    {
        base.SetCell(cell);
        textValue.text = (cell.count * cell.item.weight).ToString() + " Í„";
    }
}
