using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private CellUI cellPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private List<CellUI> cells;
    [SerializeField] private List<CellUI> cellsArmor;
    [SerializeField] private InfoPanel infoPanel;

    public List<CellUI> getCellsArmor => cellsArmor;
    public List<CellUI> getCells => cells;

    [SerializeField] private Image imageDragNDrop;
    private bool isDrag = false;

    public bool getDrag => isDrag;

    private int indexPutCell = -1;

    private Inventory inventory;

    public Cell GetCell(int index)
    {
        return inventory.getCells[index];
    }

    public void Init()
    {
        int index = 0;
        foreach (CellUI cell in cells)
        {
            cell.SetInventoryUI(this);
            cell.SetIndex(index);
            index++;
        }

    }

    public void SetCell(int index, Cell cell)
    {
        cells[index].SetCell(cell);
    }

    public void OpenPanel(CellUI cell)
    {
        infoPanel.gameObject.SetActive(true);
        infoPanel.SetCell(cell);
        infoPanel.OpenInfo();
    }

    public void LoadCells(int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            CellUI cellUI = Instantiate(cellPrefab, content);
            cells.Add(cellUI);
        }
       
        cells = cells.Concat(cellsArmor).ToList();
    }
    public void SetInventory(Inventory inv)
    {
        inventory = inv;
    }

    private void Update()
    {
        if(isDrag) imageDragNDrop.transform.position = Input.mousePosition;
    }
    public void SetPutCell(int index)
    {
        indexPutCell = index;
    }

    public void StartDrag(int indexCell)
    {
        isDrag = true;
        imageDragNDrop.gameObject.SetActive(isDrag);
        imageDragNDrop.sprite = cells[indexCell].getCell.item.imageItem;
    }

    public void StopDrag()
    {
        isDrag = false;
        imageDragNDrop.gameObject.SetActive(isDrag);
    }

    public void TryPutItemInCell(CellUI cell)
    {
        if (indexPutCell < 0) return;

        Cell oldCell = new Cell();
        Cell newCell = new Cell();
        oldCell.item = cell.getCell.item;
        oldCell.count = cell.getCell.count;
        newCell.item = cells[indexPutCell].getCell.item;
        newCell.count = cells[indexPutCell].getCell.count;


        if (cell.getCell.item == cells[indexPutCell].getCell.item)
        {
            if (cells[indexPutCell].getCell.count != cells[indexPutCell].getCell.item.maxStack
                && cell.getCell.count != cell.getCell.item.maxStack)
            {
                if (cell.getCell.count + cells[indexPutCell].getCell.count > cell.getCell.item.maxStack)
                {
                    oldCell.count = Mathf.Abs(cell.getCell.count - cells[indexPutCell].getCell.count);

                    newCell.count = newCell.item.maxStack;
                }
            }
        }

        if (cells[indexPutCell].TryGetNewItem(oldCell) == false 
            || cells[cell.getIndex].TryGetNewItem(newCell) == false) return;

        inventory.SetCell(cell.getIndex, newCell);
        inventory.SetCell(indexPutCell, oldCell);
    }

    public void DeleteItem(int indexCell)
    {
        infoPanel.gameObject.SetActive(false);
        inventory.DeleteItem(indexCell);
    }

    public void CloseInfoPanel()
    {
        infoPanel.gameObject.SetActive(false);
    }
}
