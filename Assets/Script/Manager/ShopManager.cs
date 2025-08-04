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

    //�湮�� ����
    private void SpawnVisitor()
    {
        //� �湮�ڰ� �����ߴ���
        var visitor = allVisitors[Random.Range(0, allVisitors.Count)];
        //� �ŷ��� ���ϴ���
        currentRequest = CreateTradeRequest(visitor);


        //�湮�� ������ �����ֵ��� �ϱ�
        UIManager.Instance.VisitorPanel.ShowVisitor(currentRequest);
    }    

    //�ŷ��� �����ϴ� �ܰ谡 �ʿ���
    //�ŷ��� ���� ���� �������� �����ߴ��� �ʿ���
    public void Bargaining(ItemData item, int gold)
    {
        //request�� ���ؼ� ���� ���� ��õ��� ���� 
        if(item != currentRequest.item)
        {
            //�ŷ� ����
            FailTrade();
            return;
        }

        TradeResult tr = currentRequest.GetTradeResult(gold);
        if (tr == TradeResult.SUCCESS)
        {
            //�ŷ� ����
            Debug.Log("����");
            SuccessTrade();
        }
        else if(tr == TradeResult.FAILED)
        {
            //�ŷ� ����
            Debug.Log("����");
            FailTrade();
        }
        else if(tr == TradeResult.AVAILABLE)
        {
            //�ٽ� �ŷ�
            Debug.Log("��õ�");
            UIManager.Instance.VisitorPanel.ShowVisitor(currentRequest);
        }
    }

    //�ŷ��� �Ϸ���
    public void SuccessTrade()
    {
        if(currentRequest.TradeType == TradeType.BUY)
        {
            InventoryManager.Instance.gold += currentRequest.price;
            Debug.Log($"�Ǹ� ���� {currentRequest.item.displayName} -> + {currentRequest.price}G");
        }
        else if(currentRequest.TradeType == TradeType.SELL)
        {
            InventoryManager.Instance.gold -= currentRequest.price;
            InventoryManager.Instance.AddItem(currentRequest.item);
            Debug.Log($"���� �Ϸ�: {currentRequest.item.displayName} -> -{currentRequest.price}G");
        }

        UIManager.Instance.VisitorPanel.Hide();
        NextStep();
    }

    public void FailTrade()
    {
        Debug.Log("�ŷ� ����");
        UIManager.Instance.VisitorPanel.Hide();
        NextStep();
    }

    int count = 0;
    private void NextStep()
    {
        if (count < 3)
        {
            Debug.Log("���� �湮��");
            SpawnVisitor();
            count++;
        }
    }
}
