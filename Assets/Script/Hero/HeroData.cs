using System.Collections.Generic;
using UnityEngine;

//영웅의 능력치
[System.Serializable]
public class HeroData
{
    public int level = 1;
    public int maxHP = 100;
    public int currentHP = 100;
    public int atk = 0;
    public int DefaultAtk => 20;
    public int ATK => atk + DefaultAtk;
    public int def = 0;
    public int DefaultDef => 10;
    public int DEF => def + DefaultDef;

    public Dictionary<EquipmentSlotType, ItemData> equippedItems = new();
    public List<ActiveBuff> activeBuffs = new(); 
}
