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
    public GameObject Train_prefab;

    //鉄道の出現の高さ
    public float SpwanTrain_Height;

    //コルーチンの秒数
    public int Coroutine_Trainminute;
    public int Coroutine_Rocketminute;

    //カメラからどのくらい離れているか
    public float SpwanposToCam_Angle;

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
        spwanPos = new Vector3(-50 * Mathf.Sin((AngleToOrigin+ SpwanposToCam_Angle) * Mathf.Deg2Rad), SpwanTrain_Height, -50 * Mathf.Cos((AngleToOrigin+ SpwanposToCam_Angle) * Mathf.Deg2Rad));
    }

    public void CallTrain(Vector3 _position, int _minute)
    {
        GameObject parent = new GameObject("Parent");
        parent.transform.position = new Vector3(0,0,0);
        parent.transform.parent = this.transform;
        parent.transform.Rotate(0, AngleToOrigin + SpwanposToCam_Angle, 0);


        GameObject train;
        train =  Instantiate(Train_prefab, _position, Quaternion.identity);
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
            yield return new WaitForSeconds(Coroutine_Trainminute);
        }
        
    }
    IEnumerator callRocket_constant()
    {
        while (true)
        {
            CallRocket(-spwanPos + new Vector3(Random.Range(-30, 30), -100, Random.Range(-30, 30)), Rocket_time);
            yield return new WaitForSeconds(Coroutine_Rocketminute);
        }

    }

}
