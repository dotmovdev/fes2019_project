using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScreenControl : MonoBehaviour
{
    [Header("Cameras")]
    private List<Camera> ScreenCameras = new List<Camera>();

    public int DisplayCount
    {
        get
        {
            return Display.displays.Length;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogFormat("<color=red>Display Connected: </color>{0}", DisplayCount);

        if(DisplayCount > 1)
        {
            for(int i = 1; i < DisplayCount; i++)
            {
                Display.displays[i].Activate();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
