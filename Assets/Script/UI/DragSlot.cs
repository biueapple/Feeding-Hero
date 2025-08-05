using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    private ItemSlotUI preSlot;
    public ItemSlotUI PreSlot { get { return preSlot; } set { preSlot = value; } }
    public ItemData ItemData => preSlot.ItemData;

    public Image icon;
    public TMP_Text amount;

    public void SetSprite(ItemData data)
    {
        icon.sprite = data != null ? data.icon : null;
        gameObject.SetActive(true);
    }

    public void Begin(ItemSlotUI slot)
    {
        if (slot.ItemData == null)
            return;
        //실제 데이터는 이동하지 않음
        preSlot = slot;
        SetSprite(slot.ItemData);
    }

    public void Drop(ItemSlotUI slot)
    {
        //실제로 데이터의 이동
        if(slot.Enterable(preSlot.ItemData) && preSlot.Enterable(slot.ItemData))
        {
            var (item1, item2) = (slot.ItemData, preSlot.ItemData);
            preSlot.SetItemData(item1);
            slot.SetItemData(item2);
        }
        preSlot.Refresh();
        slot.Refresh();
    }

    public void End()
    {
        //다시 리프레시만 하고 실제 데이터 이동은 없음
        preSlot.Refresh();
        gameObject.SetActive(false);
    }
}
