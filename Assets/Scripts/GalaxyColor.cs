using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ColorRange
{
    public Vector2 r;
    public Vector2 g;
    public Vector2 b;

    public ColorRange(Vector2 r, Vector2 g, Vector2 b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }
}

[CreateAssetMenu(menuName = "Galaxy/Color")]
public class GalaxyColor : ScriptableObject
{ 
    [Header("アンドロメダ銀河")]
    public Vector3 EdgeMistColor;
    public Gradient CenterMistColor;
    public Gradient SpiralColor;
    //public Vector2 BodyColor_R;
    //public Vector2 BodyColor_G;
    //public Vector2 BodyColor_B;

    public ColorRange BodyColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
