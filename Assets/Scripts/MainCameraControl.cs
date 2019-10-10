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
        this.transform.Rotate(new Vector3(0, rotateSpeed, 0));
        mainCameraRef.transform.LookAt(targetObjectTransform);
    }
}
