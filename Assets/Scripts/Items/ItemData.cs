using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    public Sprite imageItem;
    public string nameItem;
    public string item_id;
    public int maxStack;
    public float weight;

    public List<TypeCell> typesCell;

    public List<InfoParameter> infoParameters;
    public List<ItemButton> actionButtons;
}
