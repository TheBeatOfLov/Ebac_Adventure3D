using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(-.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 5;
  

    [Header("Animation Player")]
    public string boolWalk = "Walk";
    public string triggerDeath = "Death";
    public string triggerJump = "Jump";

}
