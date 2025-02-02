public class WeaponInventory : EntityInventory
{
    private WeaponCellUI choosedCellUI;

    public Weapon choosedWeapon => choosedCellUI?.getWeapon;


    public override void Init()
    {
        inventoryUI?.SetInventory(this);
        base.Init();


        InitUI();
    }

    public override void SetCell(int index, Cell cell)
    {
        cells[index].item = cell.item;
        cells[index].count = cell.count;

        inventoryUI?.SetCell(index, cell);
    }

    public void InitUI()
    {
        if (inventoryUI == null) return;

        inventoryUI?.Init();
        SetWeapon(inventoryUI.getCells[0]);

        for (int i = 0; i < cells.Count; i++)
        {
            inventoryUI?.SetCell(i, cells[i]);
        }

    }

    public void SetWeapon(CellUI choosedCell)
    {
        choosedCellUI = choosedCell as WeaponCellUI;
    }
}
