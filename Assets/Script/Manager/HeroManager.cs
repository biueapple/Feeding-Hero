using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public static HeroManager Instance { get; private set; }

    public HeroData Hero;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    //영웅이 장비를 장착함
    public void EquipFromLocker()
    {
        Debug.Log("용사가 장비를 장착함");

        var looker = InventoryManager.Instance.EquipmentLocker;
        Debug.Log(looker);
        foreach (EquipmentSlotType type in Enum.GetValues(typeof(EquipmentSlotType)))
        {
            EquipmentSlot e = InventoryManager.Instance.GetEquipmentType(type);
            Hero.equippedItems[type].Insert(e.ItemData);
            Debug.Log($"{type} 슬롯에 '{e.ItemData?.displayName}' 장착됨");
            //e에서 아이템 제거
            e.Insert(null);
        }

        ApplyEquippedStats();
    }

    //영웅이 음식을 먹음
    public void ApplyFoodBuffs(FoodAttribute food)
    {
        ApplyBuff(food.buffEffect);
        Debug.Log($"음식 섭취 -> 버프: {food.buffEffect.displayName}");
    }

    //영웅이 휴식함
    public void Rest()
    {
        Debug.Log("휴식과 회복");
        Hero.currentHP = Math.Min(Hero.maxHP, Hero.currentHP + 10);

        for(int i = Hero.activeBuffs.Count -1; i >= 0; i--)
        {
            if (Hero.activeBuffs[i].remainingDays <= 0)
            {
                Debug.Log($"버프 '{Hero.activeBuffs[i].buff.displayName}' 만료됨");
                Hero.activeBuffs.RemoveAt(i);
            }
            Hero.activeBuffs[i].remainingDays -= 1;
        }

        Hero.atk = 0;
        Hero.def = 0;
        ApplyBuffStats();
    }

    //모험의 결과 알려주기 테스트용
    public void ReceiveAdventureResult(List<string> logs)
    {
        Debug.Log("모험 결과 로그");
        foreach (var log in logs)
            Debug.Log(log);
    }



    

    //영웅의 장비 스탯을 적용
    private void ApplyEquippedStats()
    {
        int bonusAtk = 0, bonusDef = 0;
        foreach (var kvp in Hero.equippedItems)
        {
            var item = kvp.Value;
            var equipment = item.ItemData?.itemAttributes.OfType<EquipmentAttribute>().FirstOrDefault();
            if(equipment != null)
            {
                bonusAtk += equipment.atkBonus;
                bonusDef += equipment.defBonus;
            }
        }

        Debug.Log($"장비 보너스 스탯 -> ATK: {bonusAtk}, DEF: {bonusDef}");
        Hero.atk += bonusAtk;
        Hero.def += bonusDef;
    }

    //음식의 버프를 적용
    private void ApplyBuff(Buff buff)
    {
        var hero = Hero;

        if(buff.durationType == BuffDurationType.ONETIME)
        {
            hero.currentHP = Math.Min(hero.maxHP, hero.currentHP + buff.bonusHP);
            Debug.Log($"즉시 체력 회복: + {buff.bonusHP} -> {hero.currentHP} / {hero.maxHP}");
        }
        else
        {
            hero.activeBuffs.Add(new ActiveBuff
            {
                buff = buff,
                remainingDays = buff.durationDays
            });
        }
    }

    //버프의 스탯을 적용
    private void ApplyBuffStats()
    {
        int bonusAtk = 0, bonusDef = 0;
        
        foreach(var buff in Hero.activeBuffs)
        {
            bonusAtk += buff.buff.bonusAtk;
            bonusDef += buff.buff.bonusDef;
        }

        Debug.Log($"버프 보너스 스탯 -> ATK: {bonusAtk}, DEF: {bonusDef}");
        Hero.atk += bonusAtk;
        Hero.def += bonusDef;
    }
}