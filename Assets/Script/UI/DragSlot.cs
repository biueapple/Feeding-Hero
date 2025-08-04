using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    private InventorySlot preSlot;
    public InventorySlot PreSlot { get { return preSlot; } set { preSlot = value; } }
    private ItemData itemData;
    public ItemData ItemData => itemData;

    public Image icon;
    public TMP_Text amount;

    public void SetItem(ItemData data)
    {
        itemData = data;
        if (data == null)
        {
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }
        else
        {
            icon.sprite = data.icon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }
    }

}
