using TMPro;
using UnityEngine;
using UnityEngine.UI;

//visitor가 어떤 요구를 하는지 보여주는 클래스
public class UIVisitorPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private Button acceptButton;
    [SerializeField]
    private Button refuseButton;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private InventorySlot slotUI;

    private void Start()
    {
        //거절 버튼에 리스너 넣기
        refuseButton.onClick.AddListener(ShopManager.Instance.FailTrade);
    }

    public void ShowVisitor(TradeRequest request)
    {
        gameObject.SetActive(true);
        //descriptionText.text = $"{request.visitor.visitorName} 이(가) {request.item.displayName} x {request.quantity} 를 {(request.TradeType == TradeType.BUY ? "구매" : "판매")} 하려 합니다. \n가격: {request.TotalPrice}G";
        descriptionText.text = request.NowText;

        //리스턴 추가
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(AcceptCallback);

        inputField.text = "0G";
        inputField.gameObject.SetActive(true);
        if (request.TradeType == TradeType.BUY)
        {
            acceptButton.GetComponentInChildren<TMP_Text>().text = "판매";
            slotUI.gameObject.SetActive(true);
        }
        else if(request.TradeType == TradeType.SELL)
        {
            acceptButton.GetComponentInChildren<TMP_Text>().text = "구입";
            slotUI.gameObject.SetActive(false);
        }
    }

    private void AcceptCallback()
    {
        string str = inputField.text.TrimEnd('G');
        Debug.Log(str);
        if (int.TryParse(str, out int result))
        {
            Debug.Log("거래 시도");
            ShopManager.Instance.Bargaining(slotUI.ItemData, result);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        if(slotUI.ItemData != null)
        {
            InventoryManager.Instance.AddItem(slotUI.ItemData);
            slotUI.SetItem(null);
        }
    }
}
