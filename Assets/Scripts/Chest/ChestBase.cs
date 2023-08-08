using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public Animator animator;
    public string triggerOpen = "Open";
    public KeyCode keyCode = KeyCode.Z;

    [Header("Notification")]
    public GameObject notification;
    public float animationDuration = .2f;
    public Ease ease = Ease.OutBack;

    public float startScale;
    private bool _chestOpen = false;

    public ChestItemBase chestItemBase;


    private void Start()
    {
        startScale = notification.transform.localScale.x;
        HideNotification();
    }

    private void OpenChest()
    {
        if (_chestOpen) return;
        animator.SetTrigger(triggerOpen);
        _chestOpen = true;
        HideNotification();
        Invoke(nameof(ShowItems), 1f);
    }

    private void ShowItems()
    {
        chestItemBase.ShowItem();
        Invoke(nameof(CollectItems), 1f);
    }

    private void CollectItems()
    {
        chestItemBase.Collect();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if(p != null)
        {
            ShowNotification();
        }
    } 
    
    private void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if(p != null)
        {
            HideNotification();
        }
    }

    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.DOScale(startScale, animationDuration);
    }

    private void HideNotification()
    {
        notification.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();
        }
    }
}
