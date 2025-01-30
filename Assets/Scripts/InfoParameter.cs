using TMPro;
using UnityEngine;

public class InfoParameter : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textValue;
    protected Cell cell;

    public virtual void SetCell(Cell cell)
    {
        this.cell = cell;
    }

}

