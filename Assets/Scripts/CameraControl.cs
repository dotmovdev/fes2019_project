using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform targetObject;

    // Start is called before the first frame update
    void Start()
    {
        if(targetObject == null)
        {
            Debug.LogError("targetObject is Null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            mainCamera.transform.LookAt(targetObject);
        }
    }
}
