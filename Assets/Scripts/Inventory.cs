using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using System.IO;

public class Inventory : MonoBehaviour, ISaveableObject
{
    [SerializeField] private List<Cell> cells = new List<Cell>();
    [SerializeField] private List<CellArmor> cellsArmor = new List<CellArmor>();

    [SerializeField] private string filePath;
    [SerializeField] private int capacity;

    public List<Cell> getCells => cells;

    public bool autoLoad;

    private InventoryUI inventoryUI;

    public void Start()
    {
        SaveManager.instance?.AddSaveableObject(this);

        filePath = Application.persistentDataPath + "/Cells/";
        inventoryUI = FindAnyObjectByType<InventoryUI>();
        inventoryUI.SetInventory(this);

        if (autoLoad) LoadData();
        cells = cells.Concat(cellsArmor).ToList();

        InitUI();
    }

   

    public void SetCell(int index, Cell cell)
    {
        cells[index] = cell;

        inventoryUI?.SetCell(index, cell);

        if (index >= capacity)
        {
            index -= capacity;

            cellsArmor[index].item = cell.item;
            cellsArmor[index].count = cell.count;
        }
    }
  

    public void DeleteItem(int indexCell)
    {
        cells[indexCell] = new Cell();
        inventoryUI?.SetCell(indexCell, cells[indexCell]);
    }

    public void InitUI()
    {
        if (inventoryUI == null) return;

        inventoryUI?.LoadCells(capacity);
        inventoryUI?.Init();

        for (int i = 0; i < cells.Count; i++)
        {
            inventoryUI?.SetCell(i, cells[i]);
        }

    }

    public void LoadData()
    {
        cells = new List<Cell>();

        for (int i = 0; i < capacity; i++)
        {
            cells.Add(new Cell());
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
    public bool isTake => item != null;

}


[Serializable]
public class CellArmor : Cell
{
    public TypeArmor armorType;
}