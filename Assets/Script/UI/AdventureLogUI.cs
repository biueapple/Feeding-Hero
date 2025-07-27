using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//�½�Ʈ Ŭ���� ���� ������ �ؽ�Ʈ�� �����ֱ� ����
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
        Canvas.ForceUpdateCanvases(); // �ݵ�� ����
        scrollRect.verticalNormalizedPosition = 0f; // �� �Ʒ���
    }
}
