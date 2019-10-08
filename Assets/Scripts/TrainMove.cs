using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrainMove : MonoBehaviour
{
    public GameObject road_prefab;
    private GameObject Road;

    private Vector3 direction = new Vector3(0,0,0);
    private float MovingTime = 0;
    private float timeleft;
    public float interval;
    public float offset;


    //おきかえる
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        
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

            Vector3 dir = Vector3.Normalize(direction);
            CreateRoad(transform.position + (transform.forward * offset) + (transform.up * -3f), dir);

        }
       
    }

    private void CreateRoad(Vector3 _position, Vector3 _dir)
    {
        Road = Instantiate(road_prefab, _position, Quaternion.identity);
        Road.transform.rotation = Quaternion.LookRotation(_dir);
    }
    private void Do()
    {
        //終わったらデストロイ
        transform.DOMove(transform.position + direction * 20f, MovingTime).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });

    }


    public void setDirection(Vector3 _direction)
    {
        direction = _direction;
    }

    public void setMovingTime(int _movingtime)
    {
        MovingTime = _movingtime;
        Do();
    }
}
