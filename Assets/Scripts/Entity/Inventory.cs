using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public abstract class EntityInventory : MonoBehaviour, EntityPart
{
    [SerializeField] protected List<Cell> cells = new List<Cell>();
    public List<Cell> getCells => cells;

    [SerializeField] protected InventoryUI inventoryUI;
    public InventoryUI getInventoryUI => inventoryUI;

    public virtual void Init()
    {

    }

    public virtual void SetCell(int index, Cell cell)
    {
        cells[index].item = cell.item;
        cells[index].count = cell.count;


        if (cells[index].count <= 0)
        {
            DeleteItem(index);
            return;
        }

        inventoryUI?.SetCell(index, cells[index]);
    }


    public virtual void DeleteItem(int indexCell)
    {
        cells[indexCell].item = null;
        cells[indexCell].count = 0;
        inventoryUI?.SetCell(indexCell, cells[indexCell]);
    }
}


public class Inventory : EntityInventory
{

    [SerializeField] protected string filePath;
    [SerializeField] protected int capacity;


    public virtual void LoadData()
    {
        cells = new List<Cell>();

        for (int i = 0; i < capacity; i++)
        {
            Cell newCell = new Cell();
            newCell.index = i;
            cells.Add(newCell);

            if (File.Exists(filePath + $"cell{i}.json"))
            {
                string json = File.ReadAllText(filePath + $"cell{i}.json");
                cells[i] = JsonUtility.FromJson<Cell>(json);
            }
        }

    }

    public void SaveData()
    {
        int index = 0;
        foreach (Cell cell in cells)
        {
            string json = JsonUtility.ToJson(cell);
            Debug.Log(json);
            File.WriteAllText(filePath + $"cell{index}.json", json);
            index++;
        }
        Debug.Log("Данные сохранены в " + filePath);
    }

}

[Serializable]
public class Cell
{
    public int count;
    public ItemData item;
    public int index;
    public bool isTake => count > 0;

    public TypeCell typeCell;

}


[Serializable]

public enum TypeCell
{
    ArmorHead,
    ArmorBody,
    Inventory,
    Weapon
}


public interface IInventory
{
    public void SetCell(int index, Cell cell);
    public void DeleteItem(int indexCell);
}

[Serializable]
public class CellStruct<T> where T : Cell
{
    public T cell;
}
