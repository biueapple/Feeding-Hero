using UnityEngine;
using UnityEngine.UI;

//visitor�� � �䱸�� �ϴ��� �����ִ� Ŭ����
public class UIVisitorPanel : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private Button acceptButton;

    public void ShowVisitor(TradeRequest request)
    {
        gameObject.SetActive(true);
        descriptionText.text = $"{request.visitor.visitorName} ��(��) {request.item.displayName} x {request.quantity} �� {(request.TradeType == TradeType.BUY ? "����" : "�Ǹ�")} �Ϸ� �մϴ�. \n����: {request.TotalPrice}G";

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
