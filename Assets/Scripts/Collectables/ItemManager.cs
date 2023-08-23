using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

public enum ItemType
{
    COIN,
    LIFE_PACK
}

public class ItemManager : Singleton<ItemManager>
{
    public List<ItemSetup> itemSetups;

    private void Start()
    {
        Reset();
        LoadItemsFromSave();
    }

    private void LoadItemsFromSave()
    {
        AddByType(ItemType.COIN, SaveManager.Instance.setup.coins);
        AddByType(ItemType.LIFE_PACK, SaveManager.Instance.setup.lifepacks);
    }

    private void Reset()
    {
        foreach(var i in itemSetups)
        {
            i.soInt.value = 0;
        }
    }

    public ItemSetup GetItemByType(ItemType itemType, int amount = 1)
    {
        return itemSetups.Find(i => i.itemType == itemType);
    }
    public void AddByType(ItemType itemType ,int amount = 1)
    {
        if (amount < 0) return;

        itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;
    }

    public void RemoveByType(ItemType itemType, int amount = 1)
    {
     
        var item = itemSetups.Find(i => i.itemType == itemType);
        item.soInt.value -= amount;

        if (item.soInt.value < 0) item.soInt.value = 0;
    }

}

[System.Serializable]
public class ItemSetup
{
    public ItemType itemType;
    public SOInt soInt;
    public Sprite icon;
}
