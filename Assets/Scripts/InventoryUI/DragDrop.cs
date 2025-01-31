using UnityEngine;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour
{
    [SerializeField] private Image imageDragDrop;
    private bool isDrag = false;

    public bool getDrag => isDrag;

    private int indexPutCell = -1;
    private EntityInventory putInventory;


    private int indexStartCell = -1;
    private EntityInventory startInventory;

    public static DragDrop instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    private void Update()
    {
        if (isDrag) imageDragDrop.transform.position = Input.mousePosition;
    }

    public void SetPutCell(int index)
    {
        indexPutCell = index;
    }

    public void SetStartCell(int index)
    {
        indexStartCell = index;
    }

    public void SetPutInventory(EntityInventory inventory)
    {
        putInventory = inventory;
    }

    public void SetStartInventory(EntityInventory inventory)
    {
        startInventory = inventory;
    }

    public void StartDrag(int indexCell)
    {
        isDrag = true;
        imageDragDrop.gameObject.SetActive(isDrag);
        imageDragDrop.sprite = startInventory.getCells[indexCell].item.imageItem;
    }

    public void StopDrag()
    {
        isDrag = false;
        imageDragDrop.gameObject.SetActive(isDrag);
    }

    public void TryPutCell()
    {
        if (indexPutCell < 0) return;


        Cell oldCell = new Cell();
        Cell newCell = new Cell();
        oldCell.item = startInventory.getCells[indexStartCell].item;
        oldCell.count = startInventory.getCells[indexStartCell].count;
        newCell.item = putInventory.getInventoryUI.getCells[indexPutCell].getCell.item;
        newCell.count = putInventory.getInventoryUI.getCells[indexPutCell].getCell.count;


        if (startInventory.getCells[indexStartCell].item == putInventory.getCells[indexPutCell].item)
        {
            if (startInventory.getCells[indexPutCell].count != putInventory.getCells[indexPutCell].item.maxStack
                && startInventory.getCells[indexStartCell].count != putInventory.getCells[indexStartCell].item.maxStack)
            {
                if (startInventory.getCells[indexStartCell].count + putInventory.getCells[indexPutCell].count > startInventory.getCells[indexStartCell].item.maxStack)
                {
                    oldCell.count = Mathf.Abs(startInventory.getCells[indexStartCell].count - putInventory.getCells[indexPutCell].count);

                    newCell.count = newCell.item.maxStack;
                }
            }
        }

        if (startInventory.getInventoryUI.getCells[indexStartCell].TryGetNewItem(newCell) == false
            || putInventory.getInventoryUI.getCells[indexPutCell].TryGetNewItem(oldCell) == false) return;

        putInventory.SetCell(indexPutCell, oldCell);
        startInventory.SetCell(indexStartCell, newCell);

        ResetData();
    }

    public void ResetData()
    {
        putInventory = null;
        startInventory = null;
        indexPutCell = -1;
        indexStartCell = -1;
    }
}
