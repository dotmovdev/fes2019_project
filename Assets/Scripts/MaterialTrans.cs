using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTrans : MonoBehaviour
{
    private Renderer rend;
    private float countTime = 0;
    private float param;
    private float TransSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rend =  GetComponent<Renderer>();
        rend.material.SetFloat("_Param", 0);
        TransSpeed = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        if (param < 0.5 || param > 1.5)
        {
            param += TransSpeed;
            rend.material.SetFloat("_Param", param);
        }
        else if (param >= 0.5 && param <= 1.5)
        {
            param += TransSpeed/3;
            rend.material.SetFloat("_Param", param);

        }

        //countTime += 0.0001f;

        //if (countTime <= 1.1f || countTime >= 1.9f)
        //{
        //    param += TransSpeed;
        //    rend.material.SetFloat("_Param", param);
        //}
        
       

        if(param >= 2.0f)
        {
            Destroy(this.gameObject);
        }

    }

    public void setParam(float _transSpeed)
    {
        TransSpeed = _transSpeed;

    }
}
