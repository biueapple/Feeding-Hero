using UnityEngine;

public enum TradeResult
{
    SUCCESS,
    FAILED,
    AVAILABLE
}

public class TradeRequest
{
    public VisitorData visitor;
    public ItemData item;
    public int price;

    //�������� ���ݿ� ������ ���� �� ������ ���⿡�� �ʵ带 ������ �ڴ�.
    //�������� ��� (�÷��̾ �춧�� �̸�ŭ �ε� ���� �ȴ�� �̸�ŭ ��ε� ����)
    public int successRange;
    //�׳� ����
    public int failRange;
    //���° �õ�������
    private int attemptCount = 0;
    public int AttemptCount => attemptCount;

    public TradeType TradeType => visitor.tradeType;
    public string NowText { get
        {
            if (attemptCount == 0)
                return visitor.meeting;
            else
            {
                if (visitor.unsatisfactory == null)
                    return "�ؽ�Ʈ�� ������ ���� ����";
                return visitor.meeting + "\n" + visitor.unsatisfactory[Mathf.Min(visitor.unsatisfactory.Length - 1, attemptCount)];
            }
        } 
    }

    public TradeResult GetTradeResult(int gold)
    {
        attemptCount++;
        //�湮���� �춧
        if(TradeType == TradeType.BUY)
        {
            if (gold < price + successRange)    return TradeResult.SUCCESS;     //����� �δ�
            else if (gold > price + failRange)  return TradeResult.FAILED;      //�ʹ� ��δ�
            else                                return TradeResult.AVAILABLE;
        }
        //�湮���� �ȶ�
        else if(TradeType == TradeType.SELL)
        {
            if (gold > price - successRange)    return TradeResult.SUCCESS; //����� ��δ�
            else if (gold < price - failRange)  return TradeResult.FAILED; //�ʹ� �δ�
            else                                return TradeResult.AVAILABLE;
        }
        return TradeResult.FAILED;
    }
}
