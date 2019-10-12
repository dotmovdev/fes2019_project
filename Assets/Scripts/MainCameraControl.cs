using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraControl : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    private Camera mainCameraRef;
    [SerializeField]
    private float rotateSpeed = 0.015f;

    [Header("Vertical Rotation")]
    [SerializeField]
    private bool isVerticalRotationActive = true;
    [SerializeField]
    private float verticaTop;
    [SerializeField]
    private float verticalBottom;

    private int direction = 1;

    [Header("TargetObject")]
    [SerializeField]
    private Transform targetObjectTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(verticaTop < this.transform.localRotation.x)
        {
            direction = -1;
        }

        if(verticalBottom > this.transform.localRotation.x)
        {
            direction = 1;
        }

        if (isVerticalRotationActive == false)
        {
            direction = 0;
        }

        this.transform.Rotate(new Vector3(rotateSpeed * direction, rotateSpeed, 0));
        mainCameraRef.transform.LookAt(targetObjectTransform);
    }
}
