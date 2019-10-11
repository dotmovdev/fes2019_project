using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.Events;
using DG.Tweening;
using SignExtensions;

public class SignEvent : UnityEvent
{

}

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
    private float headSpeedScale = 0.5f;
    [SerializeField]
    private float spawnDuration = 1.0f;
    [SerializeField]
    private Ease easeType;
    [SerializeField]
    private string id = "";

    [Header("Event")]
    public SignEvent OnCompleteCreateSign = new SignEvent();

    private bool headToCenter = false;

    private Transform _spawnTransform;
    public Transform SpawnerTransform
    {
        get
        {
            return _spawnTransform;
        }

        set
        {
            _spawnTransform = value;
        }
    }

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

    public Vector3 VisiblePosition
    {
        get
        {
            return new Vector3(0, this.transform.localPosition.y * -1, 0);
        }
    }

    public Quaternion ZeroQuaternion
    {
        get
        {
            return new Quaternion(0, 0, 0, 0);
        }
    }

    private GameMaster gameMasterRef;

    private Vector3 CenterNormalDirection
    {
        get
        {
            var direction = this.transform.localPosition - gameMasterRef.CenterTransform.position;
            return direction.normalized;
        }
    }

    private void Start()
    {
        gameMasterRef = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    private void Update()
    {
        if (headToCenter)
        {
            this.transform.localPosition -= CenterNormalDirection * headSpeedScale;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (headToCenter)
        {
            headToCenter = false;
            this.transform.parent = other.gameObject.transform;
        }
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
        });

        onCompleteAction.Invoke();
    }

    private void createSign(Sign sign)
    {
        var signControl = instantiateSign();
        signControl.AllocateStars(sign, () =>
        {
            StartCoroutine(drawLinesCoroutine(signControl));
        });
    }

    private IEnumerator drawLinesCoroutine(SignControl signControl)
    {
        for(int i = 0; i < signControl.SignLines.Count; i++)
        {
            if (i != signControl.SignLines.Count - 1)
            {
                signControl.SignLines[i].DrawLine(() => { });
            }
            else
            {
                signControl.SignLines[i].DrawLine(() =>
                {
                    headToCenter = true;
                    //最後だけコールバック
                    OnCompleteCreateSign.Invoke();
                });
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private SignControl instantiateSign()
    {
        GameObject signObject = Instantiate(signPrefab, this.transform);
        signObject.transform.localPosition = Vector3.zero;
        signObject.transform.localRotation = ZeroQuaternion;
        return signObject.GetComponent<SignControl>();
    }
}