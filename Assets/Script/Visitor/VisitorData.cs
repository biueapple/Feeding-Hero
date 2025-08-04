using UnityEngine;

[CreateAssetMenu(menuName = "Game/Visitor")]
public class VisitorData : ScriptableObject
{
    //이름
    public string visitorName;
    //이미지
    public Sprite portrait;

    //팔지 살지
    public TradeType tradeType;
    //아이템
    public ItemData targetItem;
    //가격
    public int price;
    //성격 (얼마나 가격에 관대한지 범위)
    //이정도면 허용 (플레이어가 살때는 이만큼 싸도 성공 팔대는 이만큼 비싸도 성공)
    [Range(0, 10)]
    public int successRange;
    //그냥 실패
    [Range(0, 10)]
    public int failRange;


    //몇번 기회가 있는지
    public int chance;
    //대사 종류 (대면, 거래 성공, 실패, 불만족)
    public string meeting;
    public string success;
    public string fail;
    public string[] unsatisfactory;
}
