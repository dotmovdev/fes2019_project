using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMassElementControl : MonoBehaviour
{
    [SerializeField]
    private SphereCollider collider;

    [Header("Rotation")]
    [SerializeField]
    private float minRotateSpeed = 0.2f;
    [SerializeField]
    private float maxRotateSpeed = 1.0f;
    [SerializeField]
    private Vector3 rotateVector;

    // Start is called before the first frame update
    void Start()
    {
        //回転する速度
        rotateVector = new Vector3(
            Random.Range(minRotateSpeed, maxRotateSpeed),
            Random.Range(minRotateSpeed, maxRotateSpeed),
            0
            );
        StartCoroutine(SetColliderRandomRadius());
    }

    //ランダムに範囲を変えてばらつかせる
    private IEnumerator SetColliderRandomRadius()
    {
        while (true)
        {
            collider.radius = Random.Range(0.35f, 1.0f);
            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotateVector);
    }
}
