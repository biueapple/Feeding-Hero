using UnityEngine;

public class ItemSlot
{
    private ItemData itemData;
    public ItemData ItemData => itemData;

    public bool IsEmpty => itemData == null;

    public virtual bool Enterable(ItemData itemData)
    {
        return true;
    }

    public virtual bool Insert(ItemData itemData)
    {
        if (!Enterable(itemData)) return false;
        this.itemData = itemData;
        return true;
    }

    public bool ItemEquals(ItemData itemData)
    {
        return this.itemData == itemData;
    }
}

public class EquipmentSlot : ItemSlot
{
    public EquipmentSlotType type;
    
    public EquipmentSlot(EquipmentSlotType type)
    {
        this.type = type;
    }

    public override bool Enterable(ItemData itemData)
    {
        if (itemData == null)
            return true;
        if (itemData.TryGetAttribute(out EquipmentAttribute attr) && attr.slotType == type)
            return true;
        return false;
    }
}

public class FoodSlot : ItemSlot
{
    public override bool Enterable(ItemData itemData)
    {
        if (itemData == null)
            return true;
        if (itemData.TryGetAttribute(out FoodAttribute _))
            return true;
        return false;
    }
}