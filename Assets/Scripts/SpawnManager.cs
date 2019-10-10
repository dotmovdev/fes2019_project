using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SignExtensions;

public class SpawnManager : MonoBehaviour
{
    public bool IsSpawning
    {
        get
        {
            return isSpawning;
        }
    }

    public Transform SpawnTransform
    {
        get
        {
            return this.transform;
        }
    }

    [SerializeField]
    private bool isSpawning = false;
    [SerializeField]
    private GameObject StarSpherePrefab;

    //星座のデータを受け取って、Spawnを開始する関数
    public void StartSpawn(string id, Sign sign)
    {
        isSpawning = true;

        //星の発生源
        GameObject starSphere = Instantiate(StarSpherePrefab, this.transform);
        var starSphereControl = starSphere.GetComponent<StarSphereControl>();

        var targetPosition = new Vector3(
            0, this.transform.position.y * -1, 0
            );
        starSphereControl.Spawn(id, sign, Vector3.zero, targetPosition, () =>
        {
            isSpawning = false;
        });
    }
}
