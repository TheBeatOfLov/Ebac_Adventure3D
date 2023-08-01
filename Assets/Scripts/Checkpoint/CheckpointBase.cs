using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public int key = 1;
    //private string _checkpointKey = "Checkpoint Key";

    private bool _checkpointActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!_checkpointActive && other.transform.tag == "Player")
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        SaveCheckpoint();
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

    private void SaveCheckpoint()
    {
        /* if(PlayerPrefs.GetInt(_checkpointKey, 0) > key)
        PlayerPrefs.SetInt(_checkpointKey, key); */

        CheckpointManager.Instance.SaveCheckPoint(key);
    }
}
