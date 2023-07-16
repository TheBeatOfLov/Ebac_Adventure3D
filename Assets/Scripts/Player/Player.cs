using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public CharacterController characterController;

    private float _vSpeed = 0f;

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //Implementando movimento Pra frente e pros lados
        var speedVectorVert = transform.forward * Input.GetAxis("Vertical") * speed;
        var speedVectorHor = transform.right * Input.GetAxis("Horizontal") * speed;

        //gravidade
        _vSpeed -= gravity * Time.deltaTime;
        speedVectorVert.y = _vSpeed;
        speedVectorHor.y = _vSpeed;


        characterController.Move(speedVectorVert * Time.deltaTime);
        characterController.Move(speedVectorHor * Time.deltaTime);

        /*O prof fez assim (por rotação), mas parece um carrinho entao não curti:
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);*/

    }
}
