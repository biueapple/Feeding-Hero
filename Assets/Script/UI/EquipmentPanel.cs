using UnityEngine;

public class EquipmentPanel : MonoBehaviour, IClosableUI
{
    [SerializeField]
    private EquipmentSlotUI weapon;
    [SerializeField]
    private EquipmentSlotUI armor;
    [SerializeField]
    private EquipmentSlotUI accessory;

    public bool IsOpen => gameObject.activeSelf;

    public void ShowEquipment(EquipmentSlot[] equipmentSlots)
    {
        foreach (var slot in equipmentSlots)
        {
            if (slot.type == EquipmentSlotType.WEAPON)
                weapon.Init(slot);

            else if (slot.type == EquipmentSlotType.ARMOR)
                armor.Init(slot);

            else if (slot.type == EquipmentSlotType.ACCESSORY)
                accessory.Init(slot);
        }
        gameObject.SetActive(true);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
