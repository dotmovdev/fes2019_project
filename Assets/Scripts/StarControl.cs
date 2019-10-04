using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarControl : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    public Texture2D BackgroundTexture
    {
        set
        {
            meshRenderer.material.SetTexture("_Background", value);
        }
    }
    public Texture2D HighlightTexture
    {
        set
        {
            meshRenderer.material.SetTexture("_Highlight", value);
        }
    }
    public Color HighlightColor
    {
        set
        {
            meshRenderer.material.SetColor("_HighlightColor", value);
        }
    }
    public int BrightSeed
    {
        set
        {
            meshRenderer.material.SetFloat("_BrightSeed", value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
