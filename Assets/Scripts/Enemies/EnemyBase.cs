using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{
    public float startLife = 10f;
    private float _currentLife;

    [Header("Start Animation")]
    public float startAnimationDuration = .1f;
    public Ease startAnimationEase = Ease.OutBack;
    public bool startWithBornAnimation = true;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if (startWithBornAnimation) BornAnimation();
        ResetLife();
    }

    protected virtual void ResetLife()
    {
      _currentLife = startLife;
    }
    protected virtual void Kill()
    {
      OnKill();    
    }
    protected virtual void OnKill()
    {
        Destroy(gameObject);
    }

    public void OnDamage(float damage)
    {
        _currentLife -= damage;

        if(_currentLife <= 0)
        {
            Kill();
        }
    }

    #region ANIMATION
    private void BornAnimation()
    {
        transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
    }
    #endregion

}
