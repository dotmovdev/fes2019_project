using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaterialTrans : MonoBehaviour
{
    private Renderer rend;
    private float countTime = 0;
    private float param;
    public float TransSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rend =  GetComponent<Renderer>();
        rend.material.SetFloat("_Param", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (param < 0.5)
        {
            param += TransSpeed;
            rend.material.SetFloat("_Param", param);
        }
        else if (param > 1.5)
        {
            param += TransSpeed/2;
            rend.material.SetFloat("_Param", param);

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
}
