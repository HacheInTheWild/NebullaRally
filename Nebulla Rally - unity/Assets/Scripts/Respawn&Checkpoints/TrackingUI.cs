using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingUI : MonoBehaviour
{
    [SerializeField] private Tracking trackCheckpoints;

    void Start()
    {
        trackCheckpoints.OnPlayerCorrectCheckpoint += TrackCheckpoints_OnPlayerCorrectChekpoint;
        trackCheckpoints.OnPlayerWrongCheckpoint += TrackCheckpoints_OnPlayerWrongChekpoint;

        Hide();
    }

    public void TrackCheckpoints_OnPlayerWrongChekpoint(object sender, System.EventArgs e)
    {
        Show();
    }

    public void TrackCheckpoints_OnPlayerCorrectChekpoint(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}