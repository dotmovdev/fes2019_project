using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.Events;

public abstract class VFXBind : MonoBehaviour
{
    [SerializeField]
    protected VisualEffect vfx;

    [Header("EventName")]
    [SerializeField]
    private string playEventName = "Play";
    [SerializeField]
    private string stopEventName = "Stop";

    [Header("EventBind")]
    public UnityEvent OnPlay;
    public UnityEvent OnStop;

    protected abstract void updateVFXProperty();
    private void Update()
    {
        updateVFXProperty();
    }

    public void Play()
    {
        vfx.SendEvent(playEventName);
        OnPlay.Invoke();
    }

    public void Stop()
    {
        vfx.SendEvent(stopEventName);
        OnStop.Invoke();
    }
}