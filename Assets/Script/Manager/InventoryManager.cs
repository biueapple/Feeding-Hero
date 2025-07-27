using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    //��簡 ������ �������� �����ִ� ��
    public Dictionary<EquipmentSlotType, ItemData> equipmentLocker = new();

    //���
    public int gold;
    private Dictionary<ItemData, int> itemStacks = new();

    //�׽�Ʈ�� ���
    public ItemData testData;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        //��� ���� �׽�Ʈ �ڵ�
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
