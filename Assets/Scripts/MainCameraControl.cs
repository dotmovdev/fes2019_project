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
    private float verticaRange;
    [SerializeField]
    private float verticalRotateScale = 0.5f;
    [SerializeField]
    private float horizontalRotateScale = 0.75f;

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
        this.transform.localEulerAngles = new Vector3(
            Mathf.Sin(Time.time * verticalRotateScale) * verticaRange,
            Time.time * horizontalRotateScale, this.transform.localEulerAngles.z);

        mainCameraRef.transform.LookAt(targetObjectTransform);
    }
}
