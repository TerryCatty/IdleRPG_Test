using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour, EntityPart
{
    [SerializeField] protected Image HP_bar;
    [SerializeField] protected int maxHP;
    [SerializeField] protected int health;
    public int getHP => health;
    public int getMaxHP => maxHP;

    private float barAmount => health * amountPerHP;
    private float amountPerHP;

    private InventoryArmor inventory;

    private void Awake()
    {
        amountPerHP = 1 / (float)maxHP;
    }


    public virtual void Init()
    {
        if(TryGetComponent(out InventoryArmor inv))
        {
            inventory = inv;
        }
    }

    public void UpdateHPBar()
    {
        if (HP_bar == null) return;

        HP_bar.fillAmount = barAmount;
    }

    public void GetDamage(int damage, BodyPart bodyPartAttacked)
    {
        int armor = CheckArmor(bodyPartAttacked);

        health -= damage - armor;
        CheckHealth();
    }

    public int CheckArmor(BodyPart bodyPartAttacked)
    {
        int armor = 0;

        if(inventory != null)
        {
            if (inventory.getCells.Count > 0)
            {
                ItemArmor armorItem = null;

                foreach(CellUI cell in inventory.getInventoryUI.getCells)
                {
                    ArmorCellUI newCell = cell as ArmorCellUI;
                    if(newCell.armorType == bodyPartAttacked)
                    {
                        armorItem = newCell.getCell.item as ItemArmor;
                        break;
                    }
                }

                if(armorItem != null) armor = armorItem.protect;
            }
        }

        return armor;
    }

    public void Heal(int value)
    {
        health += value;
        CheckHealth();
    }

    public void CheckHealth()
    {
        UpdateHPBar();

        if (health <= 0)
        {
            health = 0;
            Death();

            return;
        }

        else if (health > maxHP)
        {
            health = maxHP;
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
