using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClotheItemSpeed : ClotheItemBase
{
    public float targetSpeed = 2f;
    public override void Collect()
    {
        base.Collect();
        Player.Instance.ChangeSpeed(targetSpeed, duration);
    }
}
