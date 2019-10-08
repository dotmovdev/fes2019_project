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
    private Ease easeType;

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

    public void StartSpawn(Vector3 startPosition, Vector3 targetPosition, System.Action onCompleteAction)
    {
        var spawnTween = DOTween.To(
            () => this.transform.localPosition = startPosition,
            position => this.transform.localPosition = position,
            targetPosition,
            spawnDuration);
        spawnTween.SetEase(easeType);
        spawnTween.OnComplete(() =>
        {
            var burstTween = DOTween.To(
                () => Alpha = 1.0f,
                alpha => Alpha = alpha,
                0.0f,
                0.35f
                );
            PlayBurst();
            onCompleteAction.Invoke();
        });
       
    }
}