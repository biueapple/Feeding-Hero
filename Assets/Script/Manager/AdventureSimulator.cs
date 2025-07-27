using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureSimulator : MonoBehaviour
{
    public static AdventureSimulator Instance { get; private set; }

    public event Action<string> OnLogGenerated;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    //���迡 ���� ����
    public IEnumerator RunAdventure()
    {
        Debug.Log("��簡 ������ �����մϴ�");

        var hero = HeroManager.Instance.Hero;

        for(int i = 1; i <= 3; i++)
        {
            int enemyAtk = UnityEngine.Random.Range(10, 25);
            int enemyDef = UnityEngine.Random.Range(5, 15);
            int enemyHP = UnityEngine.Random.Range(40, 80);

            Log($"���� {i}: �� ���� (HP {enemyHP}, ATK {enemyAtk}, DEF {enemyDef})");

            yield return new WaitForSeconds(1);

            //������ �� ����
            int heroAtk = UnityEngine.Random.Range(hero.ATK - 2, hero.ATK + 3); ;
            int actualHeroDamage = Mathf.Max(1, heroAtk - enemyDef);
            enemyHP -= actualHeroDamage;

            Log($" ��簡 ������ {actualHeroDamage} ���ظ� ���� (�� ���� HP {enemyHP})");

            yield return new WaitForSeconds(1);

            //���� ��������
            int damageToHero = Mathf.Max(1, enemyAtk - hero.DEF);
            damageToHero = UnityEngine.Random.Range(damageToHero - 2, damageToHero + 3);
            hero.currentHP -= damageToHero;

            Log($" ���� ��翡�� {damageToHero} ���ظ� ���� (��� ���� HP {hero.currentHP})");


            if (hero.currentHP <= 0)
            {
                hero.currentHP = 0;
                Log($" ��簡 ���������ϴ�");
                break;
            }
            yield return new WaitForSeconds(1);
        }

        Log("���� ����");
        yield return null;
    }

    private void Log(string msg)
    {
        Debug.Log(msg);
        OnLogGenerated?.Invoke(msg);
    }
}
