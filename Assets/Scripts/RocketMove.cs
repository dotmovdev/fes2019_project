using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketMove : MonoBehaviour
{

    private Vector3 goal;
    private float MovingTime = 0;

    private Vector3 goaldir;
    private Vector3 pre_pos;

    public GameObject rocket;

    // Start is called before the first frame update
    void Start()
    {
        pre_pos = this.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = this.transform.position - pre_pos;
        this.transform.rotation = Quaternion.LookRotation(direction);
        pre_pos = this.transform.position;
        rocket.transform.Rotate(0,2,0);//回転しながらすすむ
    }
    private void Do()
    {
        //終わったらデストロイ
        
        transform.DOMove
            (
            transform.position + goaldir * MovingTime * 50f, MovingTime).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
    }
   

    public void setParam(int _movingtime)
    {
        
        MovingTime = _movingtime;
        Vector3 goalPos = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));//おおよそ中心にむかってとぶ
        goaldir = (goalPos - this.gameObject.transform.position).normalized;
        Do();
    }

}
