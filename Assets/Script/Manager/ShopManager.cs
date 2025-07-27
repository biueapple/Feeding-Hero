using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [SerializeField]
    private List<VisitorData> allVisitors;

    private TradeRequest currentRequest;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    
    public void StartDayPhase()
    {
        SpawnVisitor();
    }

    private void SpawnVisitor()
    {
        var visitor = allVisitors[Random.Range(0, allVisitors.Count)];
        currentRequest = new()
        {
            visitor = visitor,
            item = visitor.targetItem,
            quantity = visitor.quantity,
            pricePerItem = visitor.pricePerItem
        };

        UIManager.Instance.VisitorPanel.ShowVisitor(currentRequest);
    }    

    public void AcceptTrade()
    {
        if(currentRequest.TradeType == TradeType.BUY)
        {
            bool success = InventoryManager.Instance.SellItem(currentRequest.item, currentRequest.quantity);
            if(success)
            {
                InventoryManager.Instance.gold += currentRequest.TotalPrice;
                Debug.Log($"판매 성공 {currentRequest.item.displayName} x {currentRequest.quantity} -> + {currentRequest.TotalPrice}G");
            }
            else
            {
                Debug.Log("재고 부족 거래 실패");
            }
        }
        else if(currentRequest.TradeType == TradeType.SELL)
        {
            if(InventoryManager.Instance.gold >= currentRequest.TotalPrice)
            {
                InventoryManager.Instance.gold -= currentRequest.TotalPrice;
                InventoryManager.Instance.AddItem(currentRequest.item, currentRequest.quantity);
                Debug.Log($"구매 완료: {currentRequest.item.displayName} x {currentRequest.quantity} -> -{currentRequest.TotalPrice}G");
            }
            else
            {
                Debug.Log("골드 부족 거래 실패");
            }
        }

        UIManager.Instance.VisitorPanel.Hide();
    }
}
