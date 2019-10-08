using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketMove : MonoBehaviour
{

    private Vector3 direction = new Vector3(0, 0, 0);
    private float MovingTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Instatiateされたら飛び続ける
        transform.rotation = Quaternion.LookRotation(direction);

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
