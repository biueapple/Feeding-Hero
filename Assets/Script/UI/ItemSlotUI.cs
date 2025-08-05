using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private ItemSlot itemSlot;
    public virtual ItemData ItemData => itemSlot?.ItemData;

    public Image icon;

    public void Init(ItemSlot itemSlot)
    {
        this.itemSlot = itemSlot;
        Refresh();
    }

    //데이터 이동 없이 스프라이트만 바꿔주는것
    public void SetSprite(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    public void Refresh()
    {
        icon.sprite = ItemData != null ? ItemData.icon : null;
    }

    public bool SetItemData(ItemData itemData)
    {
        return itemSlot.Insert(itemData);
    }

    public bool Enterable(ItemData itemData)
    {
        return itemSlot.Enterable(itemData);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        UIManager.Instance.dragSlot.Begin(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UIManager.Instance.dragSlot.GetComponent<RectTransform>().anchoredPosition =  Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIManager.Instance.dragSlot.End();
    }

    //
    public void OnDrop(PointerEventData eventData)
    {
        UIManager.Instance.dragSlot.Drop(this);
    }
}
