using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIControl : MonoBehaviour
{
    [SerializeField]
    private Renderer renderer;
    [SerializeField]
    Color color;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DynamicGI.SetEmissive(renderer, color);
    }
}
