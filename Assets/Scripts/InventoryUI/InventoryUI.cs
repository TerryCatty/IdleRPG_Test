using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] protected CellUI cellPrefab;
    [SerializeField] private Transform content;

    [SerializeField] protected List<CellUI> cells;
    public List<CellUI> getCells => cells;

    protected EntityInventory inventory;

    public EntityInventory getInventory => inventory;

    [SerializeField] protected InfoPanel infoPanel;

    public bool blockLoad;

    public virtual Cell GetCell(int index)
    {
        return inventory?.getCells[index];
    }
    public virtual void Init()
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

    public void SetInventory(EntityInventory inv)
    {
        inventory = inv;
    }

    public virtual void DeleteItem(int indexCell)
    {
        infoPanel.gameObject.SetActive(false);
        inventory.DeleteItem(indexCell);
    }

    public void OpenPanel(CellUI cell)
    {
        infoPanel.gameObject.SetActive(true);
        infoPanel.SetCell(cell);
        infoPanel.OpenInfo();
    }
    public void CloseInfoPanel()
    {
        infoPanel.gameObject.SetActive(false);
    }

    public void LoadCells(int capacity)
    {
        if (blockLoad) return;
        for (int i = 0; i < capacity; i++)
        {
            CellUI cellUI = Instantiate(cellPrefab, content);
            cells.Add(cellUI);
        }
    }


}
