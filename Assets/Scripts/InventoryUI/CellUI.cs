using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CellUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Cell cell;
    [SerializeField] protected Image image;
    [SerializeField] protected TextMeshProUGUI textCell;

    protected bool canDrag;
    protected bool enterDrag;
    protected int index;

    protected bool isHiden = false;

    public int getIndex => index;

    protected InventoryUI inventoryUI;

    public InventoryUI getInventoryUI => inventoryUI;

    public Cell getCell => cell;

    public virtual void SetCell(Cell cell)
    {
        this.cell.item = cell.item;
        this.cell.count = cell.count;

        UpdateText();
        UpdateImage();
    }

    public virtual void SetInventoryUI(InventoryUI inventory)
    {
        inventoryUI = inventory;
    }

    public virtual void SetIndex(int value)
    {
        index = value;
        cell.index = index;
    }

    public void UpdateText()
    {
        if (textCell == null) return;

        if (cell.count <= 1) textCell.text = "";
        else textCell.text = cell.count.ToString();
    }

    public void UpdateImage()
    {
        if (cell.item == null)
        {
            image.sprite = null;
            image.color = new Color(1, 1, 1, 0);
        }
        else
        {
            image.sprite = cell.item.imageItem;
            image.color = new Color(1, 1, 1, 1);
        }
    }

    public void OpenInfo()
    {
        if (cell.count <= 0) return;

        inventoryUI.OpenPanel(this);
        DragDrop.instance.StopDrag();
    }

    public void HideItem(bool value)
    {
        isHiden = !value;

        image.gameObject.SetActive(isHiden);
        textCell?.gameObject.SetActive(isHiden);

    }

    public void DeleteItem()
    {
        inventoryUI.DeleteItem(index);
    }

    public virtual bool TryGetNewItem(Cell cell)
    {
        bool result = true;

        if(cell.item != null)
        {

            if (cell.item.typesCell.Contains(this.cell.typeCell) == false)
            {
                Debug.Log(cell.item.typesCell);
                Debug.Log(this.cell.typeCell);

                result = false;
            }

        }

        return result;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       if(cell.isTake) canDrag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        HideItem(false);
        DragDrop.instance.TryPutCell();
        DragDrop.instance.StopDrag();
        canDrag = false;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (canDrag && DragDrop.instance.getDrag == false)
        {
            DragDrop.instance.SetStartInventory(inventoryUI.getInventory);
            DragDrop.instance.SetStartCell(index);
            DragDrop.instance.StartDrag(index);
            HideItem(true);
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OpenInfo();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(DragDrop.instance.getDrag)
        {
            DragDrop.instance.SetPutInventory(inventoryUI.getInventory);
            DragDrop.instance.SetPutCell(index);
            enterDrag = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        enterDrag = false;

        DragDrop.instance.SetPutCell(-1);
    }
}
