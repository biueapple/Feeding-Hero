using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string itemID;
    public string displayName;
    public Sprite icon;
    public ItemType itemType;

    public List<ItemAttribute> itemAttributes;
    public bool TryGetAttribute<T>(out T attr) where T : ItemAttribute
    {
        attr = itemAttributes?.OfType<T>().FirstOrDefault();
        return attr != null;
    }
}
