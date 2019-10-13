using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTrain_Rocket : MonoBehaviour
{
   
    public GameObject rocket_prefab; 
    private GameObject rocket;

    public GameObject camManager;
    private Vector3 spwanPos;

    private float AngleToOrigin;


    //放物線の動き検証用
    public GameObject parabora;
    public float SpwanHeight;

    //コルーチンの秒数
    public int Call_Trainminute;
    public int Call_Rocketminute;

    //カメラからどのくらい離れているか
    public float SpwanLag_Angle;

    //検証用
    public Vector3 unitVec;

    public int Train_time;
    public int Rocket_time;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(callTrain_constant());
        StartCoroutine(callRocket_constant());

    }

    // Update is called once per frame
    void Update()
    {
        AngleToOrigin = camManager.transform.rotation.eulerAngles.y ;//真ん中のはY軸で回転してる
        Vector3 camPos = new Vector3(-50*Mathf.Sin(AngleToOrigin * Mathf.Deg2Rad),0,-50* Mathf.Cos(AngleToOrigin * Mathf.Deg2Rad));
        Vector3 DirectionPos = camPos / 5 * 3.8f;
        spwanPos = new Vector3(-50 * Mathf.Sin((AngleToOrigin+ SpwanLag_Angle) * Mathf.Deg2Rad), SpwanHeight, -50 * Mathf.Cos((AngleToOrigin+ SpwanLag_Angle) * Mathf.Deg2Rad));
    }

    public void CallTrain(Vector3 _position, int _minute)
    {
        GameObject parent = new GameObject("Parent");
        parent.transform.position = new Vector3(0,0,0);
        parent.transform.parent = this.transform;
        parent.transform.Rotate(0, AngleToOrigin + SpwanLag_Angle, 0);


        GameObject train;
        train =  Instantiate(parabora, _position, Quaternion.identity);
        train.GetComponent<palaboraTrainMove>().setParam( _minute,_position);
        train.transform.parent = parent.transform;
    }

    public void CallRocket(Vector3 _position, int _minute)
    {
        rocket = Instantiate(rocket_prefab, _position, Quaternion.identity);
        rocket.GetComponent<RocketMove>().setParam(_minute);
        
    }

    IEnumerator callTrain_constant()
    {
        while (true)
        {
            CallTrain(spwanPos, Train_time);
            yield return new WaitForSeconds(Call_Trainminute);
        }
        
    }
    IEnumerator callRocket_constant()
    {
        while (true)
        {
            CallRocket(-spwanPos + new Vector3(Random.Range(-30, 30), -100, Random.Range(-30, 30)), Rocket_time);
            yield return new WaitForSeconds(Call_Rocketminute);
        }

    }

}
