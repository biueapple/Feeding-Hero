using System.Collections.Generic;
using UnityEngine;

public interface IStorage
{
    public IReadOnlyList<ItemSlot> ItemDatas { get; }
}
