using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

//하루를 구성하는 시간들
public enum DayPhase
{
    MORNING,
    DAY,
    EVENING,
    NIGHT,

}


public class DayCycleManager : MonoBehaviour
{
    //싱글톤
    public static DayCycleManager Instance { get; private set; }

    //현재 시간
    public DayPhase CurrentPhase { get; private set; }
    //몇일인지
    public int DayCount { get; private set; } = 1;

    //이벤트 구독으로 현재 시간이 달라지면 콜백해줌
    public event Action<DayPhase> OnPhaseChanged;

    //싱글톤
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
        //하루를 반복
        while (true)
        {
            yield return RunMorning();
            yield return RunDay();
            yield return RunEvening();
            yield return RunNight();
            DayCount++;
        }
    }

    //페이즈(시간대) 설정
    private void SetPhase(DayPhase phase)
    {
        CurrentPhase = phase;
        Debug.Log($"phase {phase}");
        OnPhaseChanged?.Invoke(phase);
    }

    //아침 실행
    private IEnumerator RunMorning()
    {
        SetPhase(DayPhase.MORNING);
        HeroManager.Instance.EquipFromLocker();
        yield return AdventureSimulator.Instance.RunAdventure();
    }

    //낮 실행
    private IEnumerator RunDay()
    {
        SetPhase(DayPhase.DAY);
        ShopManager.Instance.StartDayPhase();


        //거래시간은 나중에 실시간으로 할거라서 버튼은 그냥 테스트 용도로 남겨놓기
        yield return WaitForPlayerClick("다음으로");
    }

    //저녁 실행
    private IEnumerator RunEvening()
    {
        SetPhase(DayPhase.EVENING);

        UIManager.Instance.FoodUI.Open();

        bool selected = false;

        Action action = () => selected = true;
        UIManager.Instance.FoodUI.OnFoodChosen += action;
        yield return new WaitUntil(() => selected);
        UIManager.Instance.FoodUI.OnFoodChosen -= action;

        yield return WaitForPlayerClick("다음으로");
    }

    //밤 실행
    private IEnumerator RunNight()
    {
        SetPhase(DayPhase.NIGHT);
        HeroManager.Instance.Rest();
        yield return WaitForPlayerClick("다음 날 시작");
    }

    //uimanager에 연결되어 있는 버튼을 눌려야만 다음으로 넘어가도록
    private IEnumerator WaitForPlayerClick(string buttonText)
    {
        bool isClicked = false;
        UIManager.Instance.ShowNextButton(buttonText, () => isClicked = true);
        yield return new WaitUntil(() => isClicked);
        UIManager.Instance.HideNextButton();
    }
}
