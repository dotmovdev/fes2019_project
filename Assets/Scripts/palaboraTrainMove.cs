using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.VFX;

public class palaboraTrainMove : MonoBehaviour
{
    //呼び出す線路関連
    public GameObject road_prefab;
    private GameObject Road;


    private Vector3 startpos;
    public float MovingTime;
    private float timeleft = 1.0f;
    public float interval;
    public float offset;

    //materialの取得
    private Renderer[] BodyRender;

    //VFX
    public VisualEffect VFX_smog;
    public VisualEffect VFX_senro;
    private VisualEffect VFX_senroClone;
   

    //線路を呼ぶか否か。最後のほうは呼ばない。（未調整）
    private bool callRoad;

    //放物線
    public float param_a;//放物線のa
    public float param_b;
    private float pre_x;

    //方向用

    private Vector3 pre_pos;
    private Vector3 direction;

    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = this.transform.position - pre_pos;
        this.transform.rotation = Quaternion.LookRotation(direction);
        pre_pos = this.transform.position;

        //線路を一定の間隔で呼ぶ
        timeleft -= Time.deltaTime;
        if (timeleft <= 0.0)
        {
            timeleft = interval;
            if (callRoad == true)
            {
                Vector3 dir = Vector3.Normalize(direction);
                Vector3 senro_pos = transform.position + (transform.forward * offset) + (transform.up * -1.5f) + transform.right * 0.5f;
                CreateRoad(senro_pos, dir);
            }
        }
    }

    private void Do()
    {
        VFX_senroClone = Instantiate(VFX_senro, new Vector3(0, 0, 0), Quaternion.identity);
        VFX_senroClone.SetVector3("direction_VFX", new Vector3(0, 0, 0));
        VFX_senroClone.SetVector3("_pos", new Vector3(0, 0, 0));

        //二次関数的な動き
        DOTween.To
            (
                param_x => 
                {
                    Vector3 pos = new Vector3(param_x , 0 , -param_a*(param_x * param_x)+param_b);
                    this.transform.localPosition = pos;

                },
                -30,
                30,
                MovingTime
            ).OnComplete(
                    () => {
                        Destroy(VFX_senroClone.gameObject);
                        Destroy(transform.parent.gameObject);
                    }
            );
    }
 

    public void setParam(int _movingtime,  Vector3 _pos)
    {
        startpos = _pos;
        MovingTime = _movingtime;
        pre_pos = this.transform.position;
        Do();
        sound = GetComponent<AudioSource>();
        StartCoroutine(sound_onoff());
        callRoad = true;
        BodyRender = transform.Find("train/GameObject").GetComponentsInChildren<Renderer>();
        foreach (Renderer render in BodyRender)
        {
            if (render.material.HasProperty("_alpha"))
            {
                render.material.SetFloat("_alpha", 0);
                float BM = render.material.GetFloat("_alpha");
                DOTween.To
                   (
                       () => BM,
                       num => BM = num,
                       1,
                       MovingTime * 0.1f
                    ).OnUpdate(() => {
                        render.material.SetFloat("_alpha", BM);
                    }
                    ).OnComplete(
                    () => {
                        
                        Dissolve_andDestroy();
                    }
                    );//移動時間の1割の秒数で出てくる
            }
        }
    }

    private void CreateRoad(Vector3 _position, Vector3 _dir)
    {
        Road = Instantiate(road_prefab, _position, Quaternion.identity);
        Road.transform.rotation = Quaternion.LookRotation(_dir);
        Road.GetComponent<MaterialTrans>().SetTransSpeed(interval/100*6.4f);//線路の出てくるスピードをintervalを基準に指定

        VFX_senroClone.SetVector3("direction_VFX", -1 * _dir);
        float rnd = Random.Range(-0.2f,0.2f);
        VFX_senroClone.SetVector3("_pos", _position + new Vector3(rnd, 0, rnd));

    }

    //ラストの透明度
    private void Dissolve_andDestroy()
    {

        foreach (Renderer render in BodyRender)
        {
            if (render.material.HasProperty("_alpha"))
            {
                float BM = render.material.GetFloat("_alpha");
                DOTween.To
                   (
                       () => BM,
                       num => BM = num,
                       0,
                       MovingTime * 0.2f
                    ).OnUpdate(() => {
                        render.material.SetFloat("_alpha", BM);
                    }
                    
                    ).SetDelay(MovingTime - MovingTime * 0.42f
                    ).OnPlay(() => {
                        callRoad = false;
                        VFX_smog.SendEvent("OnStop");

                    });//移動時間の2割の秒数で消える
            }
        }
    }

    IEnumerator sound_onoff()
    {
        yield return new WaitForSeconds(MovingTime * 0.9f);

        sound.DOFade(
            0f,
            2
            );
    }
}