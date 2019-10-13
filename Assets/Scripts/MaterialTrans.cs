using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.VFX;

public class MaterialTrans : MonoBehaviour
{
    private Renderer rend;
    private float countTime = 0;
    private float param;
    private float TransSpeed;

    private Vector3 dir;
    public VisualEffect VFX_senro;

    // Start is called before the first frame update
    void Start()
    {
        rend =  GetComponent<Renderer>();
        rend.material.SetFloat("_Param", 0);
        
    }

    // Update is called once per frame
    void Update()
    {

        //VFX_senro.transform.position = transform.position + (transform.up * -1.5f) + (transform.right * 0.5f);
        VFX_senro.SetVector3("direction_VFX", this.gameObject.transform.forward*-1);


        if (param < 0.5)
        {
            param += TransSpeed;
            rend.material.SetFloat("_Param", param);
            
        }
        else if (param > 1.5)
        {
            param += TransSpeed;
            rend.material.SetFloat("_Param", param);
            if (param > 1.8)
            {
                VFX_senro.SendEvent("OnStop");
            }
        }

        else if (param >= 0.5 && param <= 1.5)
        {
            param += TransSpeed/3;
            rend.material.SetFloat("_Param", param);
            
        }
        if(param >= 2.0f)
        {
            Destroy(this.gameObject);
        }

    }

    public void SetTransSpeed(float _speed)
    {
        TransSpeed = _speed;

    }
    
}
