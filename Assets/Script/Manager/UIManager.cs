using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private TMPro.TextMeshProUGUI buttonText;

    private Action onClick;

    //visitor ui ����
    [SerializeField]
    private UIVisitorPanel uiVisitorPanel;
    public UIVisitorPanel VisitorPanel => uiVisitorPanel;
    //���� �����ϴ� ui ����
    [SerializeField]
    private FoodSelectionUI foodSelectionUI;
    public FoodSelectionUI FoodUI => foodSelectionUI;
    [SerializeField]
    private InventoryPanel inventoryPanel;
    public InventoryPanel InventoryPanel => inventoryPanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        nextButton.SetActive(false);
    }

    //���� �ð��볪 ������ ��ư �ؽ�Ʈ �����ֱ�
    public void ShowNextButton(string text, Action onClickCallback)
    {
        buttonText.text = text;
        onClick = onClickCallback;
        nextButton.SetActive(true);
    }

    //��ư �����
    public void HideNextButton()
    {
        nextButton.SetActive(false);
        onClick = null;
    }

    //��ư�� onclick�� ����� ��ư �������� �۵��� ��
    public void OnNextButtonClicked()
    {
        onClick?.Invoke();
    }
}
