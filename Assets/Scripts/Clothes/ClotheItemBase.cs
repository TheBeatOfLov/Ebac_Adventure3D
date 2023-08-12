using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClotheItemBase : MonoBehaviour
{
    public ClotheType clotheType;
    public string compareTag = "Player";
    public float duration = 2f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
            var setup = ClothesManager.Instance.GetClotheSetupByType(clotheType);
            Player.Instance.ChangeClothes(setup, duration);
        }
    }

    public virtual void Collect()
    {
        HideObject();
    }
    private void HideObject()
    {
        gameObject.SetActive(false);
    }


}
