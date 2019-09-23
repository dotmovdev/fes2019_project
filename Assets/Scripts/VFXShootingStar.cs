using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class VFXShootingStar : VFXBind
{
    private Vector3 prePosition = new Vector3(0, 0, 0);
    private Vector3 particleVelocity;

    private void Start()
    {
        prePosition = this.transform.position;
    }

    private void Update()
    {
        updateVFXProperty();

        particleVelocity = this.transform.position - prePosition;

        prePosition = this.transform.position;
    }

    protected override void updateVFXProperty()
    {
        vfx.SetVector3("_Position", this.transform.localPosition);
        vfx.SetVector3("_Velocity", particleVelocity.normalized);
    }
}