using UnityEngine;

public class TradeRequest
{
    public VisitorData visitor;
    public ItemData item;
    public int quantity;
    public int pricePerItem;

    public TradeType TradeType => visitor.tradeType;
    public int TotalPrice => quantity * pricePerItem;
}
