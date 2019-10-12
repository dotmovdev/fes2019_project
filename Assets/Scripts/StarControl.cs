using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarControl : MonoBehaviour
{
    [SerializeField]
    private Renderer renderer;
    [SerializeField]
    private Material material;

    public Texture2D BackgroundTexture
    {
        set
        {
            renderer.material.SetTexture("_Background", value);
        }
    }
    public Texture2D HighlightTexture
    {
        set
        {
            renderer.material.SetTexture("_Highlight", value);
        }
    }
    public Color HighlightColor
    {
        set
        {
            renderer.material.SetColor("_HighlightColor", value);
        }
    }
    public int BrightSeed
    {
        set
        {
            renderer.material.SetFloat("_BrightSeed", value);
        }
    }
    public int RenderProprity
    {
        get
        {
            return renderer.rendererPriority;
        }

        set
        {
            renderer.rendererPriority = value;
        }
    }

    public Material Material
    {
        set
        {
            renderer.material = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Material = material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
