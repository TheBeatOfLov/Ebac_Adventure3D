using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour//, IDamageable
{
    [Header("Animation")]
    public Animator animator;
    public Transform renderTransform;

    [Header("Movement")]
    public CharacterController characterController;
    public float speed = 5f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 5f;

    [Header("Running Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float runSpeed = 1.5f;

    private float _vSpeed = 0f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    public List<Collider> colliders;

    public HealthBase healthBase;

    private bool _alive = true;


    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();

        //register damage
        healthBase.OnDamage += Damage;
        healthBase.OnKill += Death;
    }

    private void Update()
    {
        Movement();
    }
    #region MOVEMENT
    private void Movement()
    {
        //Implementando movimento Pra frente e pros lados
        var inputAxisVert = Input.GetAxis("Vertical");
        var inputAxisHor = Input.GetAxis("Horizontal");

        var speedVectorVert = transform.forward * inputAxisVert * speed;
        var speedVectorHor = transform.right * inputAxisHor * speed;

        var isWalking = inputAxisVert != 0 || inputAxisHor != 0;

        //gravidade
        _vSpeed -= gravity * Time.deltaTime;
        speedVectorVert.y = _vSpeed;
        speedVectorHor.y = _vSpeed;

        //animações movement

        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVectorVert *= runSpeed;
                speedVectorHor *= runSpeed;
                animator.speed = runSpeed;
            }
            animator.SetBool("Run", true);
        }

        else
        {
            animator.SetBool("Run", false);
            animator.speed = 1;
        }

        //comando Move
        characterController.Move(speedVectorVert * Time.deltaTime);
        characterController.Move(speedVectorHor * Time.deltaTime);

      if(speedVectorHor.magnitude > 0 || speedVectorVert.magnitude > 0)
        {
            speedVectorHor.y = 0;
            speedVectorVert.y = 0;
            renderTransform.LookAt(renderTransform.position + speedVectorHor + speedVectorVert);
        }

        /*O prof fez assim (por rotação), mas parece um carrinho entao não curti
        transform.Rotate(0, inputAxisHor * turnSpeed * Time.deltaTime, 0);*/

        //jump
        if (characterController.isGrounded)
        {
            _vSpeed = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _vSpeed = jumpSpeed;
            }
        }

    }
    #endregion

    #region LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
    }

    public void Damage(float damage, Vector3 dir)
    {
        flashColors.ForEach(i => i.Flash());
    }

    // Death = OnKill (nome da do professor)
    private void Death(HealthBase h)
    {
        if(_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);
            Invoke(nameof(Revive), 3f);
        }
        
    }

    #endregion


    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetLastCheckpointPosition();
        }
    }

    public void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        Respawn();
        Invoke(nameof(TurnOnColliders), .1f);
        animator.SetTrigger("Revive");
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }
}
