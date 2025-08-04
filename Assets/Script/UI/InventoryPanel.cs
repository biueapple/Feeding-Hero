using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public InventorySlot prefab;
    private InventorySlot[] slots;
    private List<InventorySlot> slotStorage = new();

    public DragSlot dragSlot;
    
    public void ShowInventory(ItemData[] itemDatas)
    {
        int length = itemDatas.Length;
        slots = new InventorySlot[length];
        for(int i = 0; i < length; i++)
        {
            slots[i] = CreateSlot();
            slots[i].SetItem(itemDatas[i]);
        }
        gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            DestroySlot(slots[i]);
        }
        slots = null;
        gameObject.SetActive(false);
    }

    private InventorySlot CreateSlot()
    {
        InventorySlot slot = null;
        if (slotStorage.Count > 0)
        {
            slot = slotStorage[0];
            slot.gameObject.SetActive(true);
            slotStorage.RemoveAt(0);
        }
        else
        {
            slot = Instantiate(prefab, gridLayoutGroup.transform);
        }

        return slot;
    }

    private void DestroySlot(InventorySlot slot)
    {
        slot.transform.SetParent(transform);
        slot.gameObject.SetActive(false);
        slotStorage.Add(slot);
    }
}
