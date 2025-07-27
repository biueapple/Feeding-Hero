using UnityEngine;
using UnityEngine.UI;

//visitor가 어떤 요구를 하는지 보여주는 클래스
public class UIVisitorPanel : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private Button acceptButton;

    public void ShowVisitor(TradeRequest request)
    {
        gameObject.SetActive(true);
        descriptionText.text = $"{request.visitor.visitorName} 이(가) {request.item.displayName} x {request.quantity} 를 {(request.TradeType == TradeType.BUY ? "구매" : "판매")} 하려 합니다. \n가격: {request.TotalPrice}G";

        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(() =>
        {
            ShopManager.Instance.AcceptTrade();
        });
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
