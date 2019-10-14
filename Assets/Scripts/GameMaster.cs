using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SignExtensions;

public class GameMaster : MonoBehaviour, ISignCallback
{
    [Header("Sign Scale")]
    [SerializeField]
    private float scale = 0.75f;
    public float SignScale
    {
        get
        {
            return scale;
        }
    }

    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, 0);
    public Vector3 SignOffset
    {
        get
        {
            return offset;
        }
    }

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
    private StarMassControl starMassControlRef;
    public StarMassControl StarMassControlRef
    {
        get
        {
            return starMassControlRef;
        }
    }

    public Transform StarMassTransform
    {
        get
        {
            return starMassControlRef.transform;
        }
    }

    [SerializeField]
    private GalaxyColorControl galaxyColorControlRef;

    [SerializeField]
    private Transform centerTransform;
    public Transform CenterTransform
    {
        get
        {
            return centerTransform;
        }
    }

    [SerializeField]
    private SoundEffectsControl soundEffectsControlRef;
    public SoundEffectsControl SoundEffectsControlRef
    {
        get
        {
            return soundEffectsControlRef;
        }
    }

    [Header("Caches")]
    [SerializeField]
    private MaterialCacheManager lineCache;
    public MaterialCacheManager LineCache
    {
        get
        {
            return lineCache;
        }
    }

    [SerializeField]
    private MaterialCacheManager starCache;
    public MaterialCacheManager StarCache
    {
        get
        {
            return starCache;
        }
    }

    [SerializeField]
    private MaterialCacheManager starSphereCache;
    public MaterialCacheManager StarSphereCache
    {
        get
        {
            return starSphereCache;
        }
    }

    [SerializeField]
    private MaterialCacheManager nonBillboardCache;
    public MaterialCacheManager NonBillboardCache
    {
        get
        {
            return nonBillboardCache;
        }
    }

    [Header("Debug")]
    [SerializeField]
    private GameObject TestObjectTree;

    //最後に使用したSpawnerのIndex
    private int lastSpawnedIndex;

    private void Awake()
    {
        for (int i = 0; i < SignColor.StarColor.Length; i++)
        {
            starCache.Add("_HighlightColor", SignColor.StarColor[i]);
            starSphereCache.Add("_BorderColor", SignColor.StarColor[i]);
            nonBillboardCache.Add("_BorderColor", SignColor.StarColor[i]);
        }

        for (int i = 0; i < SignColor.LineColor.Length; i++)
        {
            lineCache.Add("_HighlightColor", SignColor.LineColor[i]);
        }

#if UNITY_EDITOR

#else
        MultiScreenControlRef.ActivateDisplays();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(TestObjectTree);

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

        sign.ApplyScaleFactor(scale, offset);
        SpawnManagerRefs[lastSpawnedIndex].StartSpawn(id, sign);
    }
}

public static class ServerExtention
{
    public static void ApplyScaleFactor(this ref Sign sign, float scale, Vector3 offset)
    {
        for(int i = 0; i < sign.starPositions.Length; i++)
        {
            sign.starPositions[i] = sign.starPositions[i] * scale + offset;
        }
    }
}