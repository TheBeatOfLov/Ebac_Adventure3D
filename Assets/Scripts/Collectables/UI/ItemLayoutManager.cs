using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLayoutManager : MonoBehaviour
{
    public ItemLayout prefabLayout;
    public Transform container;

    private List<ItemLayout> itemLayouts;

    private void Start()
    {
        CreateItems();
    }

    private void CreateItems()
    {
        foreach(var setup in ItemManager.Instance.itemSetups)
        {
            var item = Instantiate(prefabLayout, container);
            item.Load(setup);
            //itemLayouts.Add(item);
        }
    }
}
