using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public enum BossAction
{
    INIT,
    IDLE,
    WALK,
    ATTACK,
    DEATH
}
public class BossBase : MonoBehaviour
{
    private StateMachine<BossAction> stateMachine;

    [Header("Animation")]
    public float startAnimationDuration = .5f;
    public Ease startAnimationEase = Ease.OutBack;

    [Header("Walking")]
    public float speed = 5f;
    public List<Transform> waypoints;

    [Header("Attack")]
    public int attackAmount = 5;
    public float timeBetweenAttacks = .5f;

    public HealthBase healthBase;

    private void Awake()
    {
        Init();
        healthBase.OnKill += OnBossKill;
    }

    private void Init()
    {
        //inicializando a state machine
        stateMachine = new StateMachine<BossAction>();
        stateMachine.Init();

        //registrando os states
        stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
        stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
        stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
        stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
    }

    private void OnBossKill(HealthBase h)
    {
       SwitchState(BossAction.DEATH);
    }

    #region MOVEMENT
    public void GoToRandomPoint(Action onArrive = null)
    {
        StartCoroutine(GoToPointCorountine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
    }

    IEnumerator GoToPointCorountine(Transform t, Action onArrive = null)
    {
        while(Vector3.Distance(transform.position, t.position) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        onArrive?.Invoke(); // same as: if (onArrive != null) onArrive.Invoke()
    }
    #endregion

    #region ATTACK
    public void StartAttack(Action endCallback = null)
    {
        StartCoroutine(AttackCoroutine(endCallback));
    }

    IEnumerator AttackCoroutine(Action endCallback = null)
    {
        int attacks = 0;
        if(attacks < attackAmount)
        {
            attacks++;
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        endCallback?.Invoke();
    }
    #endregion

    #region ANIMATION
    public void StartInitAnimation()
    {
        transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
    }
    #endregion

    #region STATE MACHINE
    public void SwitchState(BossAction state)
    {
        stateMachine.SwitchState(state, this);
    }
    #endregion

    #region DEBUG   
    [NaughtyAttributes.Button]
    private void SwitchInit()
    {
        SwitchState(BossAction.INIT);
    }

    [NaughtyAttributes.Button]
    private void SwitchWalk()
    {
        SwitchState(BossAction.WALK);
    }

    [NaughtyAttributes.Button]
    private void SwitchAttack()
    {
        SwitchState(BossAction.ATTACK);
    }


    #endregion
}
