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

    //visitor ui 관리
    [SerializeField]
    private UIVisitorPanel uiVisitorPanel;
    public UIVisitorPanel VisitorPanel => uiVisitorPanel;
    //음식 선택하는 ui 관리
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

    //다음 시간대나 다음날 버튼 텍스트 보여주기
    public void ShowNextButton(string text, Action onClickCallback)
    {
        buttonText.text = text;
        onClick = onClickCallback;
        nextButton.SetActive(true);
    }

    //버튼 숨기기
    public void HideNextButton()
    {
        nextButton.SetActive(false);
        onClick = null;
    }

    //버튼의 onclick과 연결된 버튼 눌렀을때 작동할 거
    public void OnNextButtonClicked()
    {
        onClick?.Invoke();
    }


    //uiopen과 close
    private void Update()
    {
        // ESC 키 눌렀을 때 맨 위 UI 닫기
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
