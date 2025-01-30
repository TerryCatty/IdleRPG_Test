using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    public ItemData getItemData => itemData;

}
