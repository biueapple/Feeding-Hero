using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField]
    private Canvas canvas;
    public Canvas Canvas => canvas;

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
    [SerializeField]
    private EquipmentPanel equipmentPanel;
    public EquipmentPanel EquipmentPanel => equipmentPanel;

    public DragSlot dragSlot;

    private Stack<IClosableUI> stack = new();

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


    //uiopen�� close
    private void Update()
    {
        // ESC Ű ������ �� �� �� UI �ݱ�
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(stack.TryPop(out IClosableUI result))
                result.Close();
        }
    }

    public void OpenUI(IClosableUI closableUI)
    {
        closableUI.Open();
        stack.Push(closableUI);
    }

    public void CloseUI()
    {
        if(stack.TryPop(out IClosableUI result))
        {
            result.Close();
        }
    }

    public void CloseAll()
    {
        while(stack.Count > 0)
        {
            if (stack.TryPop(out IClosableUI result))
            {
                result.Close();
            }
        }
    }
}
