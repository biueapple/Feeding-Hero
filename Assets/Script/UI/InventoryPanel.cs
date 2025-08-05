using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour, IClosableUI
{
    public GridLayoutGroup gridLayoutGroup;
    public ItemSlotUI prefab;
    private ItemSlotUI[] slots;
    private List<ItemSlotUI> slotStorage = new();

    public bool IsOpen => gameObject.activeSelf;

    //이걸 호출해도 uiManager의 stack에 쌓이지 않는다는것을 알아야 함
    public void ShowInventory(ItemSlot[] itemSlots)
    {
        int length = itemSlots.Length;
        slots = new ItemSlotUI[length];
        for(int i = 0; i < length; i++)
        {
            slots[i] = CreateSlot();
            slots[i].Init(itemSlots[i]);
        }
        Open();
    }

    public int GetSlotIndex(ItemSlotUI slot)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == slot)
                return i;
        }
        return -1;
    }

    private ItemSlotUI CreateSlot()
    {
        ItemSlotUI slot = null;
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

    private void DestroySlot(ItemSlotUI slot)
    {
        slot.transform.SetParent(transform);
        slot.gameObject.SetActive(false);
        slotStorage.Add(slot);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            DestroySlot(slots[i]);
        }
        gameObject.SetActive(false);
    }
}
