using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
