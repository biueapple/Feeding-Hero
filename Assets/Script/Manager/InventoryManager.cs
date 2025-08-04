using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public Dictionary<EquipmentSlotType, ItemData> equipmentLocker = new();

    //골드
    public int gold;
    //플레이어 인벤토리인데 크기 제한을 둘지는 아직 모르겠네
    private List<ItemData> itemSlots = new ();
    public ReadOnlyCollection<ItemData> ItemSlots => itemSlots.AsReadOnly();

    public ItemData[] testItem;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        //장비 장착 테스트 코드
        //equipmentLocker[testData.itemAttributes.OfType<EquipmentAttribute>().FirstOrDefault().slotType] = testData;

        AddItem(testItem[0]);
        AddItem(testItem[1]);
        AddItem(testItem[2]);
        AddItem(testItem[3]);
    }

    public void AddItem(ItemData item)
    {
        itemSlots.Add(item);
    }

    public bool SellItem(ItemData item)
    {
        if(itemSlots.Contains(item))
        {
            itemSlots.Remove(item);
            return true;
        }

        return false;
     }

    public IEnumerable<ItemData> GetItemByAttribute<T>() where T : ItemAttribute
    {
        return itemSlots.Where(slot => slot != null && slot.itemAttributes.OfType<T>().Any());
    }
}
