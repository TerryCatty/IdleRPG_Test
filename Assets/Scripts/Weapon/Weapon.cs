using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ItemWeapon weapon;

    public ItemWeapon getWeapon => weapon;

    public void Init()
    {

    }
}
