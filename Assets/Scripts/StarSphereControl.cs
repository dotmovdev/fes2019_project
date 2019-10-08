using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using DG.Tweening;

public class StarSphereControl : MonoBehaviour
{
    [Header("Visual Effects")]
    [SerializeField]
    private VisualEffect spawnEffect;

    [Header("Sphere")]
    [SerializeField]
    private MeshRenderer sphereMeshRenderer;

    [Header("SpawnParameters")]
    [SerializeField]
    private float spawnDuration = 1.0f;
    [SerializeField]
    private Transform frontCameraTransform;

    public Color SphereColor
    {
        get
        {
            return sphereMeshRenderer.material.color;
        }

        set
        {
            sphereMeshRenderer.material.SetColor("_BorderColor", value);
        }
    }

    public float Alpha
    {
        get
        {
            return sphereMeshRenderer.material.GetFloat("_Alpha");
        }

        set
        {
            sphereMeshRenderer.material.SetFloat("_Alpha", value);
        }
    }

    private void Start()
    {

    }

    public void PlayBurst()
    {
        spawnEffect.SendEvent("OnBurst");
    }

    public void StartSpawn(Vector3 targetPosition)
    {

    }
}