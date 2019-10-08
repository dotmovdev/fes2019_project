using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_RoadCreate : MonoBehaviour
{
    public GameObject road_prefab;
    public GameObject train_prefab;
    private GameObject train;
    private GameObject road;
    private float timeleft;
    public float interval;
    public float offset;

    private bool TrainMoving = false;
    private float MovingTime;
    //検証用
    public Vector3 unitVec;
    private Vector3 RoadPosition;
    private float Count;
    // Start is called before the first frame update
    void Start()
    {
        
        //めも
        //クラス化してみたので呼び出してみる
        //DOTweenいれてみる

    }

    // Update is called once per frame
    void Update()
    {
        if (TrainMoving == true && Count <= MovingTime)
        {
            Count++;
            train.transform.position += train.transform.forward * 0.1f;

            timeleft -= Time.deltaTime;
            if (timeleft <= 0.0)
            {
                timeleft = interval;

                Vector3 dir = Vector3.Normalize(unitVec);
                CreateRoad(train.transform.position + (train.transform.forward * offset) + (train.transform.up * -3f), dir);

            }

        }

        if(Count >= MovingTime)
        {
            TrainMoving = false;
            Destroy(train);
            MovingTime = 0;

        }
    }

    private void CreateRoad(Vector3 _position,Vector3 _dir)
    {
        
        road = Instantiate(road_prefab, _position, Quaternion.identity);
        road.transform.rotation = Quaternion.LookRotation(_dir);
    }

    public void StartTrain(Vector3 _position,Vector3 _direction,float _minute)
    {
        train = Instantiate(train_prefab, _position, Quaternion.identity);
        train.transform.rotation = Quaternion.LookRotation(_direction);
        TrainMoving = true;
        MovingTime = _minute;

    }

}
