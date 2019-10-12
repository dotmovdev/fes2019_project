using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTrain_Rocket : MonoBehaviour
{
   
    public GameObject rocket_prefab; 
    private GameObject rocket;

    public GameObject camManager;
    private Vector3 spwanTrain;

    private float AngleToOrigin;


    //放物線の動き検証用
    public GameObject parabora;

    //コルーチンの秒数
    public int Call_Trainminute;

    //検証用
    public Vector3 unitVec;
    public int traintime;
    public float SpwanLag;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(callTrain_constant());

    }

    // Update is called once per frame
    void Update()
    {
        AngleToOrigin = camManager.transform.rotation.eulerAngles.y ;//真ん中のはY軸で回転してる
        Vector3 camPos = new Vector3(-50*Mathf.Sin(AngleToOrigin * Mathf.Deg2Rad),0,-50* Mathf.Cos(AngleToOrigin * Mathf.Deg2Rad));
        Vector3 DirectionPos = camPos / 5 * 3.8f;
        spwanTrain = new Vector3(-50 * Mathf.Sin((AngleToOrigin+SpwanLag) * Mathf.Deg2Rad), 0, -50 * Mathf.Cos((AngleToOrigin+SpwanLag) * Mathf.Deg2Rad));




        //スペースキーを押すと鉄道が走るサンプル
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CallTrain(spwanTrain,traintime);
        }

        //Zキーを押すとロケットがとぶサンプル
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 pos = new Vector3(0, 0, 0);
            CallRocket(pos, unitVec, 3);
        }
    }

    public void CallTrain(Vector3 _position, int _minute)
    {
        /*元
        train = Instantiate(train_prefab, _position, Quaternion.identity);
        train.GetComponent<TrainMove>().setDirection(_direction);
        train.GetComponent<TrainMove>().setMovingTime(_minute);
        */
        GameObject parent = new GameObject("Parent");
        parent.transform.position = new Vector3(0,0,0);
        parent.transform.parent = this.transform;
        parent.transform.Rotate(0, AngleToOrigin + SpwanLag, 0);


        GameObject train;
        train =  Instantiate(parabora, _position, Quaternion.identity);
        train.GetComponent<palaboraTrainMove>().setParam( _minute,_position);
        train.transform.parent = parent.transform;
        
    }

    public void CallRocket(Vector3 _position, Vector3 _direction, int _minute)
    {
        rocket = Instantiate(rocket_prefab, _position, Quaternion.identity);
        rocket.GetComponent<RocketMove>().setDirection(_direction);
        rocket.GetComponent<RocketMove>().setMovingTime(_minute);
    }

    IEnumerator callTrain_constant()
    {
        while (true)
        {

            CallTrain(spwanTrain, traintime);
            yield return new WaitForSeconds(Call_Trainminute);//100秒ごとに呼び出す…んだろうか（未検証）
        }
    }

}