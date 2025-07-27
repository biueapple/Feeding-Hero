using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//테스트용 클래스 음식을 선택하는 버튼
public class FoodButtonUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text label;
    //private FoodData food;

    
    public void SetUp(ItemData item, System.Action<FoodAttribute> onClick)
    {
        FoodAttribute food = item.itemAttributes.OfType<FoodAttribute>().FirstOrDefault();
        icon.sprite = item.icon;
        label.text = item.displayName;

        GetComponent<Button>().onClick.AddListener(() => { Debug.Log($"음식 선택 {item.displayName}"); onClick?.Invoke(food); });
    }
}
