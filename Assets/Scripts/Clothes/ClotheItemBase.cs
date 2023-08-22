using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClotheItemBase : MonoBehaviour
{
    public SFXType sfxType;
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
        PlaySFX();
    }
    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    private void PlaySFX()
    {
        SFXPool.Instance.Play(sfxType);
    }
}
