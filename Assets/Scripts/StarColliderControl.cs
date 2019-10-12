using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SphereColliderEvent : UnityEvent<Collider>
{

}

public class StarColliderControl : MonoBehaviour
{
    public SphereColliderEvent OnTriggerEnterEvent = new SphereColliderEvent();

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent.Invoke(other);
    }
}
