using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestItemCoin : ChestItemBase
{
    public int coinNumber = 5;
    public GameObject coinObject;

    private List<GameObject> _items = new List<GameObject>();

    [Header("Coin Creation")]
    public Vector2 randomRange = new Vector2(-.5f, .5f);
    public float animationDuration = 1f;
    public Ease easeCreation = Ease.OutBack;


    [Header("Coin Collect")]
    public float tweenDuration = .5f;
    public Ease easeCollect = Ease.OutBack;

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItem();

    }

    private void CreateItem()
    {
        for(int i = 0; i < coinNumber; i++)
        {
            var item = Instantiate(coinObject);
            item.transform.position = transform.position + Vector3.forward * Random.Range(randomRange.x, randomRange.y) + Vector3.right * Random.Range(randomRange.x, randomRange.y);
            item.transform.DOScale(0, animationDuration).SetEase(easeCreation).From();
            _items.Add(item);
        }
    }

    public override void Collect()
    {
        base.Collect();
        foreach(var i in _items)
        {
            i.transform.DOMoveY(2f, tweenDuration).SetRelative();
            i.transform.DOScale(0, tweenDuration / 2).SetDelay(tweenDuration / 2);
            ItemManager.Instance.AddByType(ItemType.COIN);
        }
    }
}
