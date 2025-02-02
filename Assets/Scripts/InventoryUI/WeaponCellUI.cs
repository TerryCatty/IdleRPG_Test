using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponCellUI : CellUI
{
    private Weapon weapon;
    private WeaponInventory weaponInventory;

    public Weapon getWeapon => weapon;

    public override void SetCell(Cell cell)
    {
        base.SetCell(cell);

        if(weapon != null) Destroy(weapon.gameObject);

        ItemWeapon itemWeapon = cell.item as ItemWeapon;

        if (itemWeapon != null)
        {
            weapon = Instantiate(itemWeapon.prefabWeapon, transform);
            weapon.SetWeapon(itemWeapon);
        }


    }

    public override void SetInventoryUI(InventoryUI inventory)
    {
        base.SetInventoryUI(inventory);
        weaponInventory = inventory.getInventory.GetComponent<WeaponInventory>();
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Choose weapon");
        ItemWeapon item = cell.item as ItemWeapon;
        weaponInventory.SetWeapon(this);
    }
}
