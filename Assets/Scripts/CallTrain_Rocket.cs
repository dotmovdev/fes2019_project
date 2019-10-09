using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTrain_Rocket : MonoBehaviour
{
    public GameObject train_prefab;
    public GameObject rocket_prefab;


    private GameObject train;
    private GameObject rocket;

    //検証用
    public Vector3 unitVec;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //スペースキーを押すと鉄道が走るサンプル
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = new Vector3(0, 0, 0);
            CallTrain(pos, unitVec, 10);
        }

        //Zキーを押すとロケットがとぶサンプル
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 pos = new Vector3(0, 0, 0);
            CallRocket(pos, unitVec, 10);
        }


    }

    public void CallTrain(Vector3 _position, Vector3 _direction, int _minute)
    {
        train = Instantiate(train_prefab, _position, Quaternion.identity);
        train.GetComponent<TrainMove>().setDirection(_direction);
        train.GetComponent<TrainMove>().setMovingTime(_minute);

    }
    public void CallRocket(Vector3 _position, Vector3 _direction, int _minute)
    {
        rocket = Instantiate(rocket_prefab, _position, Quaternion.identity);
        rocket.GetComponent<RocketMove>().setDirection(_direction);
        rocket.GetComponent<RocketMove>().setMovingTime(_minute);
    }

}