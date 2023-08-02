using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public ItemType itemType;

    public Collider collider;
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public GameObject graphicObject;
    float timeToHide = 3f;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void Awake()
    {
        if (particleSystem != null) particleSystem.transform.SetParent(null); 
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        if(graphicObject != null) graphicObject.SetActive(false);
        Invoke("HideObject", timeToHide);
        OnCollect();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if (collider != null) collider.enabled = false;
        if (particleSystem != null) particleSystem.Play();
        if (audioSource != null) audioSource.Play();
        ItemManager.Instance.AddByType(itemType);
    }
}
