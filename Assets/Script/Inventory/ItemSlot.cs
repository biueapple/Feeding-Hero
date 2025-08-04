using UnityEngine;

public class ItemSlot
{
    public ItemData itemData;
    public int count;

    public ItemSlot(ItemData itemData, int count)
    {
        this.itemData = itemData;
        this.count = count;
    }
}
