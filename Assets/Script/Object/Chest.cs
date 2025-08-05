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
                UIManager.Instance.OpenUI(UIManager.Instance.InventoryPanel);
                break;
            case InventoryType.HeroChest:

                break;
            case InventoryType.HeroEquipment:
                UIManager.Instance.EquipmentPanel.ShowEquipment(InventoryManager.Instance.EquipmentLocker.ToArray());
                UIManager.Instance.OpenUI(UIManager.Instance.EquipmentPanel);
                break;
            case InventoryType.FoodChest:

                break;
        }
    }
}
