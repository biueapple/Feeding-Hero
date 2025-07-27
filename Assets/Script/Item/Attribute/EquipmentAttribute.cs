using UnityEngine;

[CreateAssetMenu(menuName = "Game/ItemAttribute/EquipmentAttribute")]
public class EquipmentAttribute : ItemAttribute
{
    public EquipmentSlotType slotType;
    public int atkBonus;
    public int defBonus;
}
