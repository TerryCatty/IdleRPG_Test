
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InfoPanel : MonoBehaviour
{
    private CellUI cell;

    [SerializeField] private Image imageItem;
    [SerializeField] private TextMeshProUGUI nameItemText;

    [SerializeField] private Transform infoContent;
    [SerializeField] private Transform buttonsContent;

    private List<GameObject> spawnObjects = new List<GameObject>();

    public void SetCell(CellUI cell)
    {
        this.cell = cell;

        imageItem.sprite = cell.getCell.item.imageItem;
        nameItemText.text = cell.getCell.item.name;
    }

    public void OpenInfo()
    {
        ClearAll();
        SpawnInfo();
        SpawnButtons();
    }

    private void ClearAll()
    {
        for (int i = 0; i < spawnObjects.Count; i++)
        {
            Destroy(spawnObjects[i].gameObject);
        }

        spawnObjects.Clear();
    }

    private void SpawnInfo()
    {
       
        foreach(InfoParameter info in cell.getCell.item.infoParameters)
        {
            InfoParameter infoSpawn = Instantiate(info, infoContent);
            infoSpawn.SetCell(cell.getCell);

            spawnObjects.Add(infoSpawn.gameObject);
        }
    }

    private void SpawnButtons()
    {
        foreach (ItemButton itemButton in cell.getCell.item.actionButtons)
        {
            ItemButton button = Instantiate(itemButton, buttonsContent);
            button.SetCell(cell);

            spawnObjects.Add(button.gameObject);
        }
    }
}
