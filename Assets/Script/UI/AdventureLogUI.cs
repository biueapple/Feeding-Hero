using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//태스트 클래스 모험 내용을 텍스트로 보여주기 위한
public class AdventureLogUI : MonoBehaviour
{
    public GameObject logEntryPrefab;
    public ScrollRect scrollRect;
    public Transform logContainer;


    private void Start()
    {
        if (AdventureSimulator.Instance != null)
            AdventureSimulator.Instance.OnLogGenerated += AddLog;
    }

    private void OnDisable()
    {
        if(AdventureSimulator.Instance != null)
            AdventureSimulator.Instance.OnLogGenerated -= AddLog;
    }

    private void AddLog(string msg)
    {
        var obj = Instantiate(logEntryPrefab, logContainer);
        var txt = obj.GetComponent<TMPro.TextMeshProUGUI>();
        txt.text = msg;

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)logContainer);
        Canvas.ForceUpdateCanvases(); // 반드시 포함
        scrollRect.verticalNormalizedPosition = 0f; // 맨 아래로
    }
}
