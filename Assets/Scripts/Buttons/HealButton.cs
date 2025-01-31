using UnityEngine;

public class HealButton : ItemButton
{

    private InventoryUI inventoryUI;
    private PlayerInventory playerInventory;
    private CellUI cellUI = null;

    private PlayerHealth playerHealth;

    public override void SetCell(CellUI cell)
    {
        base.SetCell(cell);
        inventoryUI = cell.getInventoryUI;
        cellUI = cell;

        playerInventory = FindAnyObjectByType<PlayerInventory>();
        if (playerInventory.TryGetComponent(out PlayerHealth health))
        {
            playerHealth = health;
        }
    }
    protected override void AddEvent()
    {
        button.onClick.AddListener(() => Heal());
    }

    public void Heal()
    {
        if (playerHealth == null) return;

        ItemHeal item = cellUI.getCell.item as ItemHeal;

        if (item == null) return;

        if (playerHealth.getHP >= playerHealth.getMaxHP) return; 

        playerHealth.Heal(item.countHeal);

        Cell cell = cellUI.getCell;
        cell.count--;

        Debug.Log(cell.index);
        Debug.Log(cellUI.getCell.index);

        playerInventory.SetCell(cellUI.getCell.index, cell);

        inventoryUI.CloseInfoPanel();
    }
}
