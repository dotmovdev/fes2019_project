using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using DG.Tweening;
using SignExtensions;

public class StarSphereControl : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField]
    private GameObject signPrefab;

    [Header("Visual Effects")]
    [SerializeField]
    private VisualEffect spawnEffect;

    [Header("Sphere")]
    [SerializeField]
    private MeshRenderer sphereMeshRenderer;
    [SerializeField]
    private GameObject internalStar;

    [Header("SpawnParameters")]
    [SerializeField]
    private float spawnDuration = 1.0f;
    [SerializeField]
    private Ease easeType;
    [SerializeField]
    private string id = "";

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
        //一緒に内部の星も消す
        Destroy(internalStar);
    }

    public void Spawn(string id, Sign sign, Vector3 startPosition, Vector3 targetPosition, System.Action onCompleteAction)
    {
        this.id = id;

        //StarSphereを決められた位置までもっていく。
        var spawnTween = DOTween.To(
            () => this.transform.localPosition = startPosition,
            position => this.transform.localPosition = position,
            targetPosition,
            spawnDuration);
        spawnTween.SetEase(easeType);
        spawnTween.OnComplete(() =>
        {
            //星が弾ける
            var burstTween = DOTween.To(
                () => Alpha = 1.0f,
                alpha => Alpha = alpha,
                0.0f,
                0.35f
                );
            PlayBurst();

            //星座を作る
            createSign(sign);
            //コールバッグでフラグリセット
        });
    }

    private void createSign(Sign sign)
    {
        GameObject signObject = Instantiate(signPrefab);

        var signControl = signObject.GetComponent<SignControl>();
        signControl.AllocateStars(sign.starPositions, () =>
        {
            Debug.Log("<color=red>OnComplete: allocate stars!</color>");
        });
    }
}