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
    private Vector3 spawnBasePosition;
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
        enterCamera();        
    }

    public void PlayBurst()
    {
        spawnEffect.SendEvent("OnBurst");
    }

    private void enterCamera()
    {
        var tween = DOTween.To(
            () => this.transform.localPosition,
            p => this.transform.localPosition = p,
            new Vector3(spawnBasePosition.x, 0, spawnBasePosition.z),
            spawnDuration);

        tween.OnComplete(() =>
        {
            Debug.Log("OnComplete");
            PlayBurst();

            //Sphereを透明に
            DOTween.To(
                () => Alpha,
                alpha => Alpha = alpha,
                0.0f,
                0.35f);
        });
    }
}
