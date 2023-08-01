using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        TurnCheckpointOn();
    }

    [NaughtyAttributes.Button]
    private void TurnCheckpointOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    [NaughtyAttributes.Button]
    private void TurnCheckpointOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.gray);
    }
}
