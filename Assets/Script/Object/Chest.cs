using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    public InventoryType inventoryType;

    public void OnClick()
    {
        //자신의 내용물을 보여줘야 함
        switch (inventoryType)
        {
            case InventoryType.Player:
                UIManager.Instance.InventoryPanel.ShowInventory(InventoryManager.Instance.ItemSlots.ToArray());
                break;
        }
    }
}
