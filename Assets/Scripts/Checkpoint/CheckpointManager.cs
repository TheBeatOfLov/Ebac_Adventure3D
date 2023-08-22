using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckPointKey = 0;
    public List<CheckpointBase> checkpoints;

    public void Start()
    {
        var setup = SaveManager.Instance.setup;

        if(setup.checkpoint != -1)
        {
            lastCheckPointKey = setup.checkpoint;
            for(int i = 0; i < checkpoints.Count; i++)
            {
                if(checkpoints[i].key == lastCheckPointKey)
                {
                    Player.Instance.transform.position = checkpoints[i].transform.position;
                }
            }
        }
    }

    public bool HasCheckpoint()
    {
        return lastCheckPointKey > 0;
    }


    public void SaveCheckPoint(int i)
    {
        if(i > lastCheckPointKey)
        {
            lastCheckPointKey = i;
            SaveManager.Instance.SaveCheckpoint(i);
        }
    }

    public Vector3 GetLastCheckpointPosition()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckPointKey);
        return checkpoint.transform.position;
    }
}
