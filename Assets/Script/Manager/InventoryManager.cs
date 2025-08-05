using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum InventoryType
{
    Player,
    HeroChest,
    HeroEquipment,
    FoodChest,

}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    //용사가 장착할 아이템이 놓여있는 곳
    private EquipmentSlot[] equipmentLocker = new EquipmentSlot[3];
    public IReadOnlyCollection<EquipmentSlot> EquipmentLocker => equipmentLocker;

    private List<ItemData> heroChest = new();
    public IReadOnlyList<ItemData> HeroChest => heroChest;

    //골드
    public int Gold { get; set; }

    //플레이어 인벤토리인데 크기 제한을 둘지는 아직 모르겠네
    private ItemSlot[] itemSlots = new ItemSlot[10];
    public IReadOnlyList<ItemSlot> ItemSlots => itemSlots;

    private (FoodAttribute attribute, ItemSlot itemSlot) table;
    public (FoodAttribute attribute, ItemSlot itemSlot) Table => table;

    public ItemData testData;
    public ItemData[] testItem;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        for (int i = 0; i < itemSlots.Length; i++)
            itemSlots[i] = new();
        for (int i = 0; i < equipmentLocker.Length; i++)
            equipmentLocker[i] = new((EquipmentSlotType)i);
        table = new(null, new FoodSlot());

        //장비 장착 테스트 코드
        GetEquipmentType(testData.itemAttributes.OfType<EquipmentAttribute>().FirstOrDefault().slotType).Insert(testData);

        AddItem(testItem[0]);
        AddItem(testItem[1]);
        AddItem(testItem[2]);
        AddItem(testItem[3]);
    }

    public bool EquipmentItem(ItemData itemData)
    {
        if (itemData == null)
            return true;
        if (itemData.TryGetAttribute(out EquipmentAttribute attr))
        {
            return GetEquipmentType(attr.slotType).Insert(itemData);
        }
        return false;
    }

    public bool FoodItem(ItemData itemData)
    {
        if (itemData == null)
            return true;
        if(itemData.TryGetAttribute(out FoodAttribute attr) && table.itemSlot.Insert(itemData))
        {
            table.attribute = attr;
            return true;
        }
        return false;
    }

    public bool AddItem(ItemData item)
    {
        if (item == null)
            return true;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].IsEmpty)
            {
                return itemSlots[i].Insert(item);
            }
        }
        return false;
    }

    public bool AddItem(int index, ItemData itemData)
    {
        if (index >= itemSlots.Length || !itemSlots[index].IsEmpty)
            return false;
        return itemSlots[index].Insert(itemData);
    }

    public bool SellItem(ItemData item)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].ItemEquals(item))
            {
                return itemSlots[i].Insert(null);
            }
        }

        return false;
    }

    public void RemoveItem(int index)
    {
        itemSlots[index].Insert(null);
    }


    public IEnumerable<ItemData> GetItemByAttribute<T>() where T : ItemAttribute
    {
        return itemSlots.Where(slot => slot != null && slot.ItemData.itemAttributes.OfType<T>().Any()).Select(slot => slot.ItemData);
    }
    public EquipmentSlot GetEquipmentType(EquipmentSlotType type)
    {
        foreach(var slot in equipmentLocker)
        {
            if (slot.type == type)
                return slot;
        }
        return null;
    }
}
