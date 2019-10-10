using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.VFX;

public class TrainMove : MonoBehaviour
{
    public GameObject road_prefab;
    private GameObject Road;

    private Vector3 direction = new Vector3(0, 0, 0);
    private float MovingTime = 0;
    private float timeleft;
    public float interval;
    public float offset;

    private Renderer[] BodyRender;

    public VisualEffect VFX;
    private bool callRoad;//線路を呼ぶか否か。最後のほうは呼ばない。

    public AnimationCurve AC;


    // Start is called before the first frame update
    void Start()
    {
        //VFX.Play();
    }

    // Update is called once per frame
    void Update()
    {

        //Instatiateされたら走り続ける
        transform.rotation = Quaternion.LookRotation(direction);

        //線路を一定の間隔で呼ぶ
        timeleft -= Time.deltaTime;
        if (timeleft <= 0.0)
        {
            timeleft = interval;
            if (callRoad == true)
            {
                Vector3 dir = Vector3.Normalize(direction);
                CreateRoad(transform.position + (transform.forward * offset) + (transform.up * -3f), dir);

            }
        }
        
    }

    private void CreateRoad(Vector3 _position, Vector3 _dir)
    {
        Road = Instantiate(road_prefab, _position, Quaternion.identity);
        Road.transform.rotation = Quaternion.LookRotation(_dir);
    }
    private void Do()
    {
        direction.Normalize();
        //終わったらmaterialのプロパティ"_alpha"をさげて最後デストロイする
        //止まってから変化するより最後のほう、動きながらのほうがいい？
        transform.DOMove
            (
            transform.position + direction * MovingTime * 10f, MovingTime).SetEase(AC
            );

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
                    ).OnComplete(() => {
                        
                        Destroy(this.gameObject);
                    }
                    ).SetDelay(MovingTime - MovingTime * 0.22f
                    ).OnPlay(() => {
                        callRoad = false;
                        VFX.SendEvent("OnStop");

                    });//移動時間の2割の秒数で消える
            }
        }
    }



    public void setDirection(Vector3 _direction)
    {
        direction = _direction;
    }

    public void setMovingTime(int _movingtime)
    {
        MovingTime = _movingtime;
        Do();
        VFX.SendEvent("OnPlay");
        Debug.Log("VFX");
        callRoad = true;


        //最後の色変化。マテリアルを１にしておく。(startだと処理が後回しにされる)
        
        BodyRender = transform.Find("GameObject").GetComponentsInChildren<Renderer>();

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
                       MovingTime * 0.05f
                    ).OnUpdate(() => {
                        render.material.SetFloat("_alpha", BM);
                    }).OnComplete(
                    () => {
                        Dissolve_andDestroy();
                        
                    }
                    );//移動時間の1割の秒数で出てくる
            }
        }

    }


    //始点と終点を指定するタイプのメモ
    //private void Do()
    //{
    //    //終わったらデストロイ
    //    transform.DOMove(Goal_pos, MovingTime).OnComplete(() =>
    //    {
    //        Destroy(this.gameObject);
    //    }).OnUpdate(() =>
    //    {
    //        //進行方向を向く
    //        Vector3 dirVec = this.gameObject.transform.position - pre_pos;
    //        Debug.Log(pre_pos);
    //        Debug.Log(this.gameObject.transform.position);
    //        direction = dirVec.normalized;
    //        pre_pos = this.transform.position;

    //        //Instatiateされたら走り続ける
    //        transform.rotation = Quaternion.LookRotation(direction);
    //    });
    //}

    
}
