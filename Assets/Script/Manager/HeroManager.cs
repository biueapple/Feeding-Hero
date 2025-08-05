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

    //������ ��� ������
    public void EquipFromLocker()
    {
        Debug.Log("��簡 ��� ������");

        var looker = InventoryManager.Instance.EquipmentLocker;
        Debug.Log(looker);
        foreach (EquipmentSlotType type in Enum.GetValues(typeof(EquipmentSlotType)))
        {
            EquipmentSlot e = InventoryManager.Instance.GetEquipmentType(type);
            Hero.equippedItems[type].Insert(e.ItemData);
            Debug.Log($"{type} ���Կ� '{e.ItemData?.displayName}' ������");
            //e���� ������ ����
            e.Insert(null);
        }

        ApplyEquippedStats();
    }

    //������ ������ ����
    public void ApplyFoodBuffs(FoodAttribute food)
    {
        ApplyBuff(food.buffEffect);
        Debug.Log($"���� ���� -> ����: {food.buffEffect.displayName}");
    }

    //������ �޽���
    public void Rest()
    {
        Debug.Log("�޽İ� ȸ��");
        Hero.currentHP = Math.Min(Hero.maxHP, Hero.currentHP + 10);

        for(int i = Hero.activeBuffs.Count -1; i >= 0; i--)
        {
            if (Hero.activeBuffs[i].remainingDays <= 0)
            {
                Debug.Log($"���� '{Hero.activeBuffs[i].buff.displayName}' �����");
                Hero.activeBuffs.RemoveAt(i);
            }
            Hero.activeBuffs[i].remainingDays -= 1;
        }

        Hero.atk = 0;
        Hero.def = 0;
        ApplyBuffStats();
    }

    //������ ��� �˷��ֱ� �׽�Ʈ��
    public void ReceiveAdventureResult(List<string> logs)
    {
        Debug.Log("���� ��� �α�");
        foreach (var log in logs)
            Debug.Log(log);
    }



    

    //������ ��� ������ ����
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

        Debug.Log($"��� ���ʽ� ���� -> ATK: {bonusAtk}, DEF: {bonusDef}");
        Hero.atk += bonusAtk;
        Hero.def += bonusDef;
    }

    //������ ������ ����
    private void ApplyBuff(Buff buff)
    {
        var hero = Hero;

        if(buff.durationType == BuffDurationType.ONETIME)
        {
            hero.currentHP = Math.Min(hero.maxHP, hero.currentHP + buff.bonusHP);
            Debug.Log($"��� ü�� ȸ��: + {buff.bonusHP} -> {hero.currentHP} / {hero.maxHP}");
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

    //������ ������ ����
    private void ApplyBuffStats()
    {
        int bonusAtk = 0, bonusDef = 0;
        
        foreach(var buff in Hero.activeBuffs)
        {
            bonusAtk += buff.buff.bonusAtk;
            bonusDef += buff.buff.bonusDef;
        }

        Debug.Log($"���� ���ʽ� ���� -> ATK: {bonusAtk}, DEF: {bonusDef}");
        Hero.atk += bonusAtk;
        Hero.def += bonusDef;
    }
}