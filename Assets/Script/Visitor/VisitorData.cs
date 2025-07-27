using UnityEngine;

[CreateAssetMenu(menuName = "Game/Visitor")]
public class VisitorData : ScriptableObject
{
    public string visitorName;
    public Sprite portrait;

    public TradeType tradeType;
    public ItemData targetItem;
    public int quantity;
    public int pricePerItem;
}
