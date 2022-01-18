using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{

    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;

    private List<Checkpoint> checkpointSingleList;
    private int nextCheckpointSingleIndex;

    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");

        foreach(Transform checkpointSingleTransform in checkpointsTransform)
        {
            Checkpoint checkpointSingle = checkpointSingleTransform.GetComponent<Checkpoint>();
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointSingleList.Add(checkpointSingle);
        }

        nextCheckpointSingleIndex = 0;
    }

    public void PlayerThroughCheckpoint(Checkpoint checkpointSingle)
    {
        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            nextCheckpointSingleIndex = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        } else
        {
            Debug.Log("Wrgon");
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
        }

    }
}
