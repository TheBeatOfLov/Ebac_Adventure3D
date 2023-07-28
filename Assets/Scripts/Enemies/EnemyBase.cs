using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] private float _currentLife;
     public float startLife = 10f;

    [Header("Start Animation")]
    public float startAnimationDuration = .1f;
    public Ease startAnimationEase = Ease.OutBack;
    public bool startWithBornAnimation = true;

    [Header("Animation")]
    [SerializeField] private AnimationBase _animationBase;

    [Header("Death Animation")]
    public float timeToDestroy = 3f;

    public Collider colliderDamage;
    public FlashColor flashColor;

    [Header("Death Particles")]
    public ParticleSystem particleSystemGoo;
    public int numberOfParticlesDeath = 10;
    public int numberOfParticlesHit = 5;

    [Header("Damage settings")]
    public float damageOnTouch = 1;

    public bool lookAtPlayer = false;
    private Player _player;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
    }

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
        if (colliderDamage != null) colliderDamage.enabled = false;
        if (particleSystemGoo != null) particleSystemGoo.Emit(numberOfParticlesDeath);
        Destroy(gameObject, timeToDestroy);
        PlayAnimationByTrigger(AnimationType.DEATH);
    }

    public void OnDamage(float damage)
    {
        if (flashColor != null) flashColor.Flash();
        if (particleSystemGoo != null) particleSystemGoo.Emit(numberOfParticlesHit);
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

    public void PlayAnimationByTrigger(AnimationType animationType)
    {
        _animationBase.PlayAnimationByTrigger(animationType);
    }
    #endregion
    public void Damage(float damage)
    {
        OnDamage(damage);
    }
    public void Damage(float damage, Vector3 dir)
    {
        OnDamage(damage);
        transform.DOMove(transform.position - dir, .1f);
    }

    //Se houver colisão com o player -> dano
    private void OnCollisionEnter(Collision collision)
    {
        Player p = collision.transform.GetComponent<Player>();
        if(p != null)
        {
            p.Damage(damageOnTouch);
        }
    }

    public virtual void Update()
    {
        if (lookAtPlayer)
        {
            transform.LookAt(_player.transform.position);
        }
    }
}
