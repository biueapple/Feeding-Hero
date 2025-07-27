using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    //용사가 장착할 아이템이 놓여있는 곳
    public Dictionary<EquipmentSlotType, ItemData> equipmentLocker = new();

    //골드
    public int gold;
    private Dictionary<ItemData, int> itemStacks = new();

    //테스트용 장비
    public ItemData testData;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        //장비 장착 테스트 코드
        equipmentLocker[testData.itemAttributes.OfType<EquipmentAttribute>().FirstOrDefault().slotType] = testData;
        Debug.Log(equipmentLocker.Count);
    }

    public void AddItem(ItemData item, int amount)
    {
        if (!itemStacks.ContainsKey(item))
            itemStacks[item] = 0;

        itemStacks[item] += amount;
    }

    public bool SellItem(ItemData item, int amount)
    {
        if (!itemStacks.ContainsKey(item)) return false;
        if (itemStacks[item] < amount) return false;

        itemStacks[item] -= amount;
        return true;
     }

    public IEnumerable<ItemData> GetItemByAttribute<T>() where T : ItemAttribute
    {
        return itemStacks.Keys.Where(item => item.itemAttributes.OfType<T>().Any());
    }
}
