﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SignExtensions;

public class GameMaster : MonoBehaviour, ISignCallback
{
    [Header("References")]
    [SerializeField]
    private MainCameraControl mainCameraControlRef;
    public MainCameraControl MainCameraControlRef
    {
        get
        {
            return mainCameraControlRef;
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

    [SerializeField]
    private SignStorage signStorageRef;
    public SignStorage SignStorageRef
    {
        get
        {
            return signStorageRef;
        }
    }

    [SerializeField]
    private List<SpawnManager> spawnManagerRefs = new List<SpawnManager>();
    public List<SpawnManager> SpawnManagerRefs
    {
        get
        {
            return spawnManagerRefs;
        }
    }

    [SerializeField]
    private Transform centerTransform;
    public Transform CenterTransform
    {
        get
        {
            return centerTransform;
        }
    }

    //最後に使用したSpawnerのIndex
    private int lastSpawnedIndex;

    private void Awake()
    {
#if UNITY_EDITOR

#else
        MultiScreenControlRef.ActivateDisplays();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        //星座を保存・管理しておくクラスから通知を受け取る
        SignStorageRef.SetOnReleaceListener(this);
        //最初は0からスタートしたい
        lastSpawnedIndex = SpawnManagerRefs.Count - 1;

#if UNITY_EDITOR

#else
        MultiScreenControlRef.FixSecondaryWindow();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReceived(string id, Sign sign)
    {
        //SpawnerのIndexを更新
        lastSpawnedIndex++;

        if(lastSpawnedIndex > SpawnManagerRefs.Count - 1)
        {
            lastSpawnedIndex = 0;
        }

        Debug.LogFormat("<color=red>[Received]</color> ID: {0}, Spawner: {1}", id, lastSpawnedIndex);

        SpawnManagerRefs[lastSpawnedIndex].StartSpawn(id, sign);
    }
}
