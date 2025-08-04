using TMPro;
using UnityEngine;
using UnityEngine.UI;

//visitor�� � �䱸�� �ϴ��� �����ִ� Ŭ����
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
        //���� ��ư�� ������ �ֱ�
        refuseButton.onClick.AddListener(ShopManager.Instance.FailTrade);
    }

    public void ShowVisitor(TradeRequest request)
    {
        gameObject.SetActive(true);
        //descriptionText.text = $"{request.visitor.visitorName} ��(��) {request.item.displayName} x {request.quantity} �� {(request.TradeType == TradeType.BUY ? "����" : "�Ǹ�")} �Ϸ� �մϴ�. \n����: {request.TotalPrice}G";
        descriptionText.text = request.NowText;

        //������ �߰�
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(AcceptCallback);

        inputField.text = "0G";
        inputField.gameObject.SetActive(true);
        if (request.TradeType == TradeType.BUY)
        {
            acceptButton.GetComponentInChildren<TMP_Text>().text = "�Ǹ�";
            slotUI.gameObject.SetActive(true);
        }
        else if(request.TradeType == TradeType.SELL)
        {
            acceptButton.GetComponentInChildren<TMP_Text>().text = "����";
            slotUI.gameObject.SetActive(false);
        }
    }

    private void AcceptCallback()
    {
        string str = inputField.text.TrimEnd('G');
        Debug.Log(str);
        if (int.TryParse(str, out int result))
        {
            Debug.Log("�ŷ� �õ�");
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
