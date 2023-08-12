using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    [SerializeField] private float _currentLife;
    public float startLife = 10f;

    [Header("Death Time")]
    public bool destroyOnKill = false;
    public float timeToDestroy = 3f;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public UIFillUpdater uIFillUpdater;

    public float damageMultiplier = 1f;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }
    protected virtual void Kill()
    {
       if(destroyOnKill)
        Destroy(gameObject, timeToDestroy);

        OnKill?.Invoke(this); 
    }

    //DEBUG KILL

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(10);
    }
   
    public void Damage(float damage)
    {
        _currentLife -= damage * damageMultiplier; //damage multiplier pra mudar quando o personagem for sofrer mais ou menos dano. O default é 1;

        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if(uIFillUpdater != null)
        {
            uIFillUpdater.UpdateValue((float)_currentLife / startLife); 
        }
    }

    public void ChangeDamageMultiplier(float multiplier, float duration)
    {
        StartCoroutine(ChangeDamageMultiplierCoroutine(multiplier, duration));
    }

    IEnumerator ChangeDamageMultiplierCoroutine(float multiplier, float duration)
    {
        damageMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        damageMultiplier = 1;
    }
}
