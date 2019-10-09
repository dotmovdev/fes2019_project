using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private MultiCameraControl multiCameraControlRef;
    public MultiCameraControl MultiCameraControlRef
    {
        get
        {
            return multiCameraControlRef;
        }
    }

    [SerializeField]
    private MultiScreenControl multiScreenControlRef;
    public MultiScreenControl MultiScreenControlRef
    {
        get
        {
            return multiScreenControlRef;
        }
    }

    private void Awake()
    {
        MultiScreenControlRef.ActivateDisplays();
    }

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR

#else
        //マルチスクリーン対応
        MultiScreenControlRef.FixSecondaryWindow();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
