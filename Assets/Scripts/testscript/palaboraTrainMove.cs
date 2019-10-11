using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.VFX;

public class palaboraTrainMove : MonoBehaviour
{
    
    private Vector3 startpos;
    public float MovingTime;
    private float timeleft;
    public float interval;
    public float offset;

    private Renderer[] BodyRender;

    public VisualEffect VFX;
    private bool callRoad;//線路を呼ぶか否か。最後のほうは呼ばない。

    public AnimationCurve AC;


    private Vector3 pre_pos;

    //追加
    public float param_a;//放物線のa
    public float param_b;
    private float pre_x;


    // Start is called before the first frame update
    void Start()
    {
        
        startpos = transform.position;
        pre_pos = this.transform.position;
        Do();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.LookRotation( this.transform.position- pre_pos);
        pre_pos = this.transform.position;
    }

    private void Do()
    {
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
                        GameObject parent = transform.parent.gameObject;
                        Destroy(parent);
                    }

            );
    }
 

    public void setParam(int _movingtime,  Vector3 _pos)
    {
        //pre_x = Mathf.Sqrt(Mathf.Abs(_pos.z - param_b) / param_a);
        //startpos = _pos;
        //Debug.Log(_pos);
        MovingTime = _movingtime;
        Do();
    }


}