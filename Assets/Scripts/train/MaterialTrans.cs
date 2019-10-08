using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTrans : MonoBehaviour
{
    private Renderer rend;
    private float countTime = 0;
    private float param;
    // Start is called before the first frame update
    void Start()
    {
        rend =  GetComponent<Renderer>();
        rend.material.SetFloat("_Param", 0);
    }

    // Update is called once per frame
    void Update()
    {
        countTime += 0.005f;

        if (countTime <= 1.2f || countTime >= 1.5f)
        {
            param += 0.005f;
            rend.material.SetFloat("_Param", param);
        }
        
        

        if(param >= 2.0f)
        {
            Destroy(this.gameObject);

        }

    }
}
