using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
public class ItemWeapon : ItemData
{
    public int damage;
    public ItemData ammo;
}
