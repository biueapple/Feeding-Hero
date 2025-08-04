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

        //�巡�׿� �������� �����ִ� ���� ���� �� ���°Ŵ� �ϴ� �κ��� �־��ְ� �Ѿ��
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
        //���� ���������� �ǵ��� ����
        if (drag.PreSlot.itemData == null)
        {
            drag.PreSlot.SetItem(drag.ItemData);
            drag.SetItem(null);
        }
        //�Ұ����ϴٸ� �׳� ȹ������ ��ȯ
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

        //�̹� �������� ������ �ִµ� �ٸ� �������� ���� �� ���� �ٽ� ���ư��� ��
        if (itemData != null && itemData != drag.ItemData)
        {
            //���� ���������� �ǵ��� ����
            if (drag.PreSlot.itemData == null)
            {
                drag.PreSlot.SetItem(drag.ItemData);
                drag.SetItem(null);
            }
            //�Ұ����ϴٸ� �׳� ȹ������ ��ȯ
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
