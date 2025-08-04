using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

//�Ϸ縦 �����ϴ� �ð���
public enum DayPhase
{
    MORNING,
    DAY,
    EVENING,
    NIGHT,

}


public class DayCycleManager : MonoBehaviour
{
    //�̱���
    public static DayCycleManager Instance { get; private set; }

    //���� �ð�
    public DayPhase CurrentPhase { get; private set; }
    //��������
    public int DayCount { get; private set; } = 1;

    //�̺�Ʈ �������� ���� �ð��� �޶����� �ݹ�����
    public event Action<DayPhase> OnPhaseChanged;

    //�̱���
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(DayCycle());
    }

    private IEnumerator DayCycle()
    {
        //�Ϸ縦 �ݺ�
        while (true)
        {
            yield return RunMorning();
            yield return RunDay();
            yield return RunEvening();
            yield return RunNight();
            DayCount++;
        }
    }

    //������(�ð���) ����
    private void SetPhase(DayPhase phase)
    {
        CurrentPhase = phase;
        Debug.Log($"phase {phase}");
        OnPhaseChanged?.Invoke(phase);
    }

    //��ħ ����
    private IEnumerator RunMorning()
    {
        SetPhase(DayPhase.MORNING);
        HeroManager.Instance.EquipFromLocker();
        yield return AdventureSimulator.Instance.RunAdventure();
    }

    //�� ����
    private IEnumerator RunDay()
    {
        SetPhase(DayPhase.DAY);
        ShopManager.Instance.StartDayPhase();


        //�ŷ��ð��� ���߿� �ǽð����� �ҰŶ� ��ư�� �׳� �׽�Ʈ �뵵�� ���ܳ���
        yield return WaitForPlayerClick("��������");
    }

    //���� ����
    private IEnumerator RunEvening()
    {
        SetPhase(DayPhase.EVENING);

        UIManager.Instance.FoodUI.Open();

        bool selected = false;

        Action action = () => selected = true;
        UIManager.Instance.FoodUI.OnFoodChosen += action;
        yield return new WaitUntil(() => selected);
        UIManager.Instance.FoodUI.OnFoodChosen -= action;

        yield return WaitForPlayerClick("��������");
    }

    //�� ����
    private IEnumerator RunNight()
    {
        SetPhase(DayPhase.NIGHT);
        HeroManager.Instance.Rest();
        yield return WaitForPlayerClick("���� �� ����");
    }

    //uimanager�� ����Ǿ� �ִ� ��ư�� �����߸� �������� �Ѿ����
    private IEnumerator WaitForPlayerClick(string buttonText)
    {
        bool isClicked = false;
        UIManager.Instance.ShowNextButton(buttonText, () => isClicked = true);
        yield return new WaitUntil(() => isClicked);
        UIManager.Instance.HideNextButton();
    }
}
