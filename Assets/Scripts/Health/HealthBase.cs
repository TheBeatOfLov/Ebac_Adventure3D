using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [SerializeField] private float _currentLife;
    public float startLife = 10f;

    [Header("Death Time")]
    public bool destroyOnKill = false;
    public float timeToDestroy = 3f;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    protected virtual void ResetLife()
    {
        _currentLife = startLife;
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
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Kill();
        }
        OnDamage?.Invoke(this);
    }
}
