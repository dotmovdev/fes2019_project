using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMassControl : MonoBehaviour
{
    [SerializeField]
    private List<SphereCollider> colliders;

    [SerializeField]
    private GameObject starMassElementPrefab;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject starMassElement = Instantiate(starMassElementPrefab, this.transform);
            starMassElement.transform.position = Vector3.zero;
        }
    }

    private void Update()
    {
        
    }
}
