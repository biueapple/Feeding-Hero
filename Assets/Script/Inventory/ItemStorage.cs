using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemStorage : IStorage
{
    private ItemSlot[] itemSlots;
    public IReadOnlyList<ItemSlot> ItemDatas => itemSlots;

    public ItemStorage(ItemSlot[] itemSlots)
    {
        this.itemSlots = itemSlots;
    }
}
