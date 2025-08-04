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

    private TradeRequest CreateTradeRequest(VisitorData visitorData)
    {
        return new()
        {
            visitor = visitorData,
            item = visitorData.targetItem,
            price = visitorData.price,
            successRange = visitorData.successRange,
            failRange = visitorData.failRange
        };
    }

    //방문자 생성
    private void SpawnVisitor()
    {
        //어떤 방문자가 도착했는지
        var visitor = allVisitors[Random.Range(0, allVisitors.Count)];
        //어떤 거래를 원하는지
        currentRequest = CreateTradeRequest(visitor);


        //방문자 정보를 보여주도록 하기
        UIManager.Instance.VisitorPanel.ShowVisitor(currentRequest);
    }    

    //거래를 흥정하는 단계가 필요함
    //거래에 얼마의 돈과 아이템을 제시했는지 필요함
    public void Bargaining(ItemData item, int gold)
    {
        //request와 비교해서 실패 성공 재시도로 나뉨 
        if(item != currentRequest.item)
        {
            //거래 실패
            FailTrade();
            return;
        }

        TradeResult tr = currentRequest.GetTradeResult(gold);
        if (tr == TradeResult.SUCCESS)
        {
            //거래 성공
            Debug.Log("성공");
            SuccessTrade();
        }
        else if(tr == TradeResult.FAILED)
        {
            //거래 실패
            Debug.Log("실패");
            FailTrade();
        }
        else if(tr == TradeResult.AVAILABLE)
        {
            //다시 거래
            Debug.Log("재시도");
            UIManager.Instance.VisitorPanel.ShowVisitor(currentRequest);
        }
    }

    //거래를 완료함
    public void SuccessTrade()
    {
        if(currentRequest.TradeType == TradeType.BUY)
        {
            InventoryManager.Instance.gold += currentRequest.price;
            Debug.Log($"판매 성공 {currentRequest.item.displayName} -> + {currentRequest.price}G");
        }
        else if(currentRequest.TradeType == TradeType.SELL)
        {
            InventoryManager.Instance.gold -= currentRequest.price;
            InventoryManager.Instance.AddItem(currentRequest.item);
            Debug.Log($"구매 완료: {currentRequest.item.displayName} -> -{currentRequest.price}G");
        }

        UIManager.Instance.VisitorPanel.Hide();
        NextStep();
    }

    public void FailTrade()
    {
        Debug.Log("거래 실패");
        UIManager.Instance.VisitorPanel.Hide();
        NextStep();
    }

    int count = 0;
    private void NextStep()
    {
        if (count < 3)
        {
            Debug.Log("다음 방문자");
            SpawnVisitor();
            count++;
        }
    }
}
