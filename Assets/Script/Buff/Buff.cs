using UnityEngine;

[CreateAssetMenu(menuName = "Game/Buff")]
public class Buff : ScriptableObject
{
    public string buffID;
    public string displayName;

    public int bonusHP;
    public int bonusAtk;
    public int bonusDef;

    public BuffDurationType durationType;
    public int durationDays;    //daily
}
