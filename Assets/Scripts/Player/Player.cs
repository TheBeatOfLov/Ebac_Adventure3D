using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Animation")]
    public Animator animator;

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

    private void Update()
    {
        Movement();
    }

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
}
