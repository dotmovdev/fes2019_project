using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.VFX;

public class RocketMove : MonoBehaviour
{

    private Vector3 goal;
    private float MovingTime = 0;

    private Vector3 goaldir;
    private Vector3 pre_pos;

    public GameObject rocket;
    public VisualEffect Rocket_VFX_prefab;
    private VisualEffect RocketVFX;

    private AudioSource sound;
    private bool soundoff;

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
        sound = GetComponent<AudioSource>();
        
        StartCoroutine(sound_onoff());
        //終わったらデストロイ

        transform.DOMove
            (
            transform.position + goaldir * MovingTime * 50f, MovingTime)
            .OnComplete(() =>
            {
                Destroy(RocketVFX.gameObject);
                Destroy(this.gameObject);

            })
            .OnUpdate(
            () =>
            {
                float rand = Random.Range(-20,20);
                RocketVFX.SetVector3("pos",this.gameObject.transform.position);
                RocketVFX.SetVector3("direction", rocket.transform.forward + new Vector3(rand, rand, rand));
                
            }
            );
    }
   

    public void setParam(int _movingtime)
    {
        
        MovingTime = _movingtime;
        Vector3 goalPos = new Vector3(Random.Range(10, 100), 0, Random.Range(10, 100));//おおよそ中心にむかってとぶ
        goaldir = (goalPos - this.gameObject.transform.position).normalized;
        Do();
        RocketVFX = Instantiate(Rocket_VFX_prefab,new Vector3(0,0,0),Quaternion.identity);
    }

    IEnumerator sound_onoff()
    {
        LoopMusicPlayer LMP = GetComponent<LoopMusicPlayer>();
        
        yield return new WaitForSeconds(MovingTime * 0.3f);

        
        LMP.FadeOut();

    }


}
