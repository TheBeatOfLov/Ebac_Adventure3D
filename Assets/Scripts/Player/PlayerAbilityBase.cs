using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityBase : MonoBehaviour
{
    protected Player player;

    private void OnValidate()
    {
        if (player != null) GetComponent<Player>();
    }

    private void Start()
    {
        OnValidate();
    }

    protected virtual void Init() { }
    protected virtual void RegisterListeners() { }
    protected virtual void RemoveListeners() { }
}
