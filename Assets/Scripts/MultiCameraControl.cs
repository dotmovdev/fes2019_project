using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCameraControl : MonoBehaviour
{
    public Camera[] Cameras
    {
        get
        {
            var cameras = GetComponentsInChildren<Camera>();
            return cameras;
        }
    }

    public Camera FrontCamera
    {
        get
        {
            return frontCamera;
        }
    }

    public Camera SideCamera
    {
        get
        {
            return sideCamera;
        }
    }

    //public List<Transform> SpawnTransforms

    [Header("Camera")]
    [SerializeField]
    private Camera frontCamera;
    [SerializeField]
    private Camera sideCamera;

    [Header("Rotation")]
    [SerializeField]
    private float rotationSpeed;

    [Header("Spawn")]
    private List<Transform> spawnTransforms = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
