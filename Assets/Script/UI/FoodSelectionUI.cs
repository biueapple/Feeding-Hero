using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//�׽�Ʈ�� Ŭ����
public class FoodSelectionUI : MonoBehaviour
{
    [SerializeField]
    private Transform container;
    [SerializeField]
    private GameObject foodButtonPrefab;

    public event System.Action OnFoodChosen;

    public void Open()
    {
        gameObject.SetActive(true);

        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }

        foreach(var food in InventoryManager.Instance.GetItemByAttribute<FoodAttribute>())
        {
            var go = Instantiate(foodButtonPrefab, container);
            go.GetComponent<FoodButtonUI>().SetUp(food, OnFoodSelection);
        }
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnFoodSelection(FoodAttribute food)
    {
        //var food = item.itemAttributes.OfType<FoodAttribute>().FirstOrDefault();
        //Debug.Log($"���� ���õ� {item.displayName}");
        HeroManager.Instance.ApplyFoodBuffs(food);
        OnFoodChosen?.Invoke();
        Close();
    }

}
