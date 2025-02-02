using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour, EntityPart
{
    [SerializeField] protected Entity targetAttack;
    [SerializeField] protected BodyPart attackPart;

    protected WeaponInventory _weaponInventory;
    public void Init()
    {
        _weaponInventory = GetComponent<WeaponInventory>();
    }


    public void Attack()
    {
        if (targetAttack == null || _weaponInventory?.choosedWeapon == null) return;

        if(targetAttack.TryGetComponent(out EntityHealth entityHealth))
        {
            entityHealth.GetDamage(_weaponInventory.choosedWeapon.getWeapon.damage, attackPart);
        }
    }
}
