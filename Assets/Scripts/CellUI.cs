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
        this.cell = cell;
        UpdateText();
        UpdateImage();
    }

    public void SetInventoryUI(InventoryUI inventory)
    {
        inventoryUI = inventory;
    }

    public virtual void SetIndex(int value)
    {
        index = value;
    }

    public void UpdateText()
    {
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
        inventoryUI.StopDrag();
    }

    public void HideItem(bool value)
    {
        isHiden = !value;

        image.gameObject.SetActive(isHiden);
        textCell.gameObject.SetActive(isHiden);

    }

    public void DeleteItem()
    {
        inventoryUI.DeleteItem(index);
    }

    public virtual bool TryGetNewItem(Cell cell)
    {
        bool result = true;
        return result;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       if(cell.isTake) canDrag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        HideItem(false);
        inventoryUI.TryPutItemInCell(this);
        inventoryUI.StopDrag();
        canDrag = false;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (canDrag && inventoryUI.getDrag == false) 
        { 
            inventoryUI.StartDrag(index);
            HideItem(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OpenInfo();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(inventoryUI.getDrag)
        {
            inventoryUI.SetPutCell(index);
            enterDrag = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        enterDrag = false;

        inventoryUI.SetPutCell(-1);
    }
}
