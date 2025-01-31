using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour, EntityPart
{
    [SerializeField] protected Entity targetAttack;
    [SerializeField] protected BodyPart attackPart;
    [SerializeField] protected List<Weapon> weaponList = new List<Weapon>();
    protected Weapon choosedWeapon;
    public void Init()
    {
       if(weaponList.Count > 0) choosedWeapon = weaponList[0];
    }


    public void Attack()
    {
        if (targetAttack == null || choosedWeapon == null || weaponList.Count == 0) return;

        if(targetAttack.TryGetComponent(out EntityHealth entityHealth))
        {
            entityHealth.GetDamage(choosedWeapon.getWeapon.damage, attackPart);
        }
    }
}
