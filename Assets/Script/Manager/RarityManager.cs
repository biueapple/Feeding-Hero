using System.Collections.Generic;
using UnityEngine;

public enum ItemRarity
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
    LEGENDARY
}

public class RarityManager : MonoBehaviour
{
    public static RarityManager Instance { get; private set; }

    public Dictionary<ItemRarity, Color> RarityColor = new()
    {
        { ItemRarity.COMMON,    Color.gray },
        { ItemRarity.UNCOMMON,  Color.green },
        { ItemRarity.RARE,      Color.blue },
        { ItemRarity.EPIC,      new Color(0.6f,0,0.8f) }, //º¸¶ó»ö
        { ItemRarity.LEGENDARY, Color.yellow }
    };
}
