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

    //협상으로 가격에 변동이 있을 수 있으니 여기에도 필드를 만들어야 겠다.
    //이정도면 허용 (플레이어가 살때는 이만큼 싸도 성공 팔대는 이만큼 비싸도 성공)
    public int successRange;
    //그냥 실패
    public int failRange;
    //몇번째 시도중인지
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
                    return "텍스트가 정해져 있지 않음";
                return visitor.meeting + "\n" + visitor.unsatisfactory[Mathf.Min(visitor.unsatisfactory.Length - 1, attemptCount)];
            }
        } 
    }

    public TradeResult GetTradeResult(int gold)
    {
        attemptCount++;
        //방문인이 살때
        if(TradeType == TradeType.BUY)
        {
            if (gold < price + successRange)    return TradeResult.SUCCESS;     //충분히 싸다
            else if (gold > price + failRange)  return TradeResult.FAILED;      //너무 비싸다
            else                                return TradeResult.AVAILABLE;
        }
        //방문인이 팔때
        else if(TradeType == TradeType.SELL)
        {
            if (gold > price - successRange)    return TradeResult.SUCCESS; //충분히 비싸다
            else if (gold < price - failRange)  return TradeResult.FAILED; //너무 싸다
            else                                return TradeResult.AVAILABLE;
        }
        return TradeResult.FAILED;
    }
}
