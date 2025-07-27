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

    //모험에 대한 내용
    public IEnumerator RunAdventure()
    {
        Debug.Log("용사가 모험을 시작합니다");

        var hero = HeroManager.Instance.Hero;

        for(int i = 1; i <= 3; i++)
        {
            int enemyAtk = UnityEngine.Random.Range(10, 25);
            int enemyDef = UnityEngine.Random.Range(5, 15);
            int enemyHP = UnityEngine.Random.Range(40, 80);

            Log($"전투 {i}: 적 등장 (HP {enemyHP}, ATK {enemyAtk}, DEF {enemyDef})");

            yield return new WaitForSeconds(1);

            //영웅의 적 공격
            int heroAtk = UnityEngine.Random.Range(hero.ATK - 2, hero.ATK + 3); ;
            int actualHeroDamage = Mathf.Max(1, heroAtk - enemyDef);
            enemyHP -= actualHeroDamage;

            Log($" 용사가 적에게 {actualHeroDamage} 피해를 입힘 (적 남은 HP {enemyHP})");

            yield return new WaitForSeconds(1);

            //적의 영웅공격
            int damageToHero = Mathf.Max(1, enemyAtk - hero.DEF);
            damageToHero = UnityEngine.Random.Range(damageToHero - 2, damageToHero + 3);
            hero.currentHP -= damageToHero;

            Log($" 적이 용사에게 {damageToHero} 피해를 입힘 (용사 남은 HP {hero.currentHP})");


            if (hero.currentHP <= 0)
            {
                hero.currentHP = 0;
                Log($" 용사가 쓰러졌습니다");
                break;
            }
            yield return new WaitForSeconds(1);
        }

        Log("모험 종료");
        yield return null;
    }

    private void Log(string msg)
    {
        Debug.Log(msg);
        OnLogGenerated?.Invoke(msg);
    }
}
