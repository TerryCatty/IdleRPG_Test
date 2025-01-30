using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] protected Button button;
    protected CellUI cell;

    public virtual void SetCell(CellUI cell)
    {
        this.cell = cell;

        AddEvent();
    }

    protected virtual void AddEvent()
    {
    }
}
