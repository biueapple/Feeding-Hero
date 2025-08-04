using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private ItemData itemData;
    public ItemData ItemData => itemData;

    public Image icon;

    public void SetItem(ItemData data)
    {
        itemData = data;
        if (data == null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
        else
        {
            icon.sprite = data.icon;
            icon.enabled = true;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ItemData == null)
            return;

        //드래그에 아이템이 남아있는 경우는 있을 수 없는거니 일단 인벤에 넣어주고 넘어가기
        DragSlot dragSlot = UIManager.Instance.InventoryPanel.dragSlot;
        if (dragSlot.ItemData != null)
        {
            InventoryManager.Instance.AddItem(dragSlot.ItemData);
            dragSlot.SetItem(null);
            return;
        }

        dragSlot.SetItem(ItemData);
        SetItem(null);
        dragSlot.PreSlot = this;
    }

    public void OnDrag(PointerEventData eventData)
    {
        UIManager.Instance.InventoryPanel.dragSlot.transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot drag = UIManager.Instance.InventoryPanel.dragSlot;
        if (drag.ItemData == null)
        {
            return;
        }
        //이전 시작점으로 되돌아 가기
        if (drag.PreSlot.itemData == null)
        {
            drag.PreSlot.SetItem(drag.ItemData);
            drag.SetItem(null);
        }
        //불가능하다면 그냥 획득으로 변환
        else
        {
            InventoryManager.Instance.AddItem(drag.ItemData);
        }
    }

    //
    public void OnDrop(PointerEventData eventData)
    {
        DragSlot drag = UIManager.Instance.InventoryPanel.dragSlot;
        if (drag.ItemData == null)
        {
            return;
        }

        //이미 아이템을 가지고 있는데 다른 아이템을 놓을 순 없음 다시 돌아가야 함
        if (itemData != null && itemData != drag.ItemData)
        {
            //이전 시작점으로 되돌아 가기
            if (drag.PreSlot.itemData == null)
            {
                drag.PreSlot.SetItem(drag.ItemData);
                drag.SetItem(null);
            }
            //불가능하다면 그냥 획득으로 변환
            else
            {
                InventoryManager.Instance.AddItem(drag.ItemData);
            }
        }
        else
        {
            SetItem(drag.ItemData);
            drag.SetItem(null);
        }
    }
}
