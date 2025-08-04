using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    public InventoryType inventoryType;

    public void OnClick()
    {
        //�ڽ��� ���빰�� ������� ��
        switch (inventoryType)
        {
            case InventoryType.Player:
                UIManager.Instance.InventoryPanel.ShowInventory(InventoryManager.Instance.ItemSlots.ToArray());
                break;
        }
    }
}
