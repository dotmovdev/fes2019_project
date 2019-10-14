using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using DG.Tweening;

public class StarMassControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameMaster gameMasterRef;
    [SerializeField]
    private VisualEffect centralVisualEffect;
    private Transform centralVFXTransform
    {
        get
        {
            return centralVisualEffect.transform;
        }
    }
    [SerializeField]
    private GameObject starMassElementPrefab;
    [SerializeField]
    private List<StarMassElementControl> massElementControlRefs = new List<StarMassElementControl>();

    [Header("Durations")]
    [SerializeField]
    private float sphereCollectDuration = 1.0f;
    [SerializeField]
    private float burstChargeDuration = 4.0f;
    [SerializeField]
    private float burstDuration = 5.0f;

    [Header("Setting")]
    [SerializeField]
    private int capacity = 10;

    private bool burstStars = false;

    public int SignCount
    {
        get
        {
            int count = 0;

            foreach(var element in massElementControlRefs)
            {
                var childSigns = element.GetComponentsInChildren<StarSphereControl>();
                count += childSigns.Length;
            }

            return count;
        }
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject starMassElement = Instantiate(starMassElementPrefab, this.transform);
            starMassElement.transform.position = Vector3.zero;

            var controlRef = starMassElement.GetComponent<StarMassElementControl>();
            massElementControlRefs.Add(controlRef);
        }
    }

    private void Update()
    {
        this.transform.localPosition = new Vector3(0, 0, 0);

        if(burstStars == false && SignCount > capacity - 1)
        {
            burstSpheres();
        }
    }

    private IEnumerator burstSphereCoroutine()
    {
        burstStars = true;

        var spheres = SetStarMassTransform();

        //中央に寄せる
        foreach (var sphere in spheres)
        {
            var tween = DOTween.To(
                () => sphere.transform.localPosition,
                (position) => sphere.transform.localPosition = position,
                new Vector3(0, 0, 0),
                sphereCollectDuration);
        }

        Debug.Log("<color=red>Collect spheres.</color>");
        yield return new WaitForSeconds(sphereCollectDuration);

        //中央のやつをぎゅーっと縮める

        //効果音 いい効果音があったら
        var soundEffects = gameMasterRef.SoundEffectsControlRef;
        //soundEffects.PlayOneShot(soundEffects.StarBurstImpactSound, 0.65f);

        var chargeTween = DOTween.To(
            () => centralVisualEffect.GetFloat("_SpawnRadiusScale"),
            (scale) =>
            {
                centralVisualEffect.SetFloat("_SpawnRadiusScale", scale);
            },
            0.1f, burstChargeDuration);

        yield return new WaitForSeconds(burstChargeDuration);

        foreach(var sphere in spheres)
        {
            Vector3 addVector = getRandomDirection();
            sphere.OnUpdateEvent.AddListener(() =>
            {
                sphere.transform.localPosition += addVector;
            });
        }

        //バースト開始
        var burstTween = DOTween.To(
            () => centralVisualEffect.GetFloat("_AttractionSpeed"),
            (speed) => centralVisualEffect.SetFloat("_AttractionSpeed", speed),
            -40.0f, burstDuration);

        //効果音
        soundEffects.PlayOneShot(soundEffects.StarBurstSound, burstDuration);

        burstTween.onComplete = () =>
        {
            var spawnScaleTween = DOTween.To(
                () => centralVisualEffect.GetFloat("_SpawnRadiusScale"),
                (scale) =>
                {
                    centralVisualEffect.SetFloat("_SpawnRadiusScale", scale);
                },
                1.0f, burstChargeDuration / 2);
            spawnScaleTween.onComplete = () =>
            {
                burstStars = false;
            };

            var finishBurstTween = DOTween.To(
                () => centralVisualEffect.GetFloat("_AttractionSpeed"),
                (speed) => centralVisualEffect.SetFloat("_AttractionSpeed", speed),
                5.0f, burstDuration);
            finishBurstTween.onComplete = () =>
            {
                foreach (var sphere in spheres)
                {
                    Destroy(sphere.gameObject);
                }
            };
        };
    }

    private void burstSpheres()
    {
        StartCoroutine(burstSphereCoroutine());
    }

    private List<StarSphereControl> SetStarMassTransform()
    {
        var controls = new List<StarSphereControl>();

        foreach (var element in massElementControlRefs)
        {
            var childSigns = element.GetComponentsInChildren<StarSphereControl>();

            foreach (var childSign in childSigns)
            {
                childSign.transform.parent = centralVFXTransform;
                controls.Add(childSign);
            }
        }

        return controls;
    }

    private Vector3 getRandomDirection()
    {
        var v = Vector3.zero;

        var cameraTransform = gameMasterRef.MainCameraControlRef.transform;
        var cameraDirection = cameraTransform.localPosition - v;
        var normalizedDirection = cameraDirection.normalized;

        normalizedDirection.x += Random.Range(-0.15f, 0.15f);
        normalizedDirection.y += Random.Range(-0.15f, 0.15f);

        return normalizedDirection;
    }
}
