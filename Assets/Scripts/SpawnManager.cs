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

    private static GameMaster gameMasterRef;

    [SerializeField]
    private bool isSpawning = false;
    [SerializeField]
    private GameObject StarSpherePrefab;

    private void Start()
    {
        gameMasterRef = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    //星座のデータを受け取って、Spawnを開始する関数
    public void StartSpawn(string id, Sign sign)
    {
        isSpawning = true;

        //星の発生源
        GameObject starSphere = Instantiate(StarSpherePrefab, this.transform);
        starSphere.transform.localRotation = new Quaternion(0, 0, 0, 0);
        var starSphereControl = starSphere.GetComponent<StarSphereControl>();
        starSphereControl.ColorIndex = sign.colorIndex;
        starSphereControl.SphereMeshRenderer.material = gameMasterRef.StarSphereCache[sign.colorIndex];
        starSphereControl.StarMeshRenderer.material = gameMasterRef.StarCache[sign.colorIndex];

        var targetPosition = new Vector3(
            0, this.transform.localPosition.y * -1, 0
            );
        starSphereControl.Spawn(id, sign, Vector3.zero, targetPosition, () =>
        {
            Debug.LogFormat("<color=red>SpawnSignID: {0}</color>", id);
        });

        //効果音を再生する
        var soundEffects = gameMasterRef.SoundEffectsControlRef;
        soundEffects.PlayOneShot(soundEffects.SpawnSound, 1.25f);

        //フラグのリセットを任せる
        starSphereControl.OnCompleteCreateSign.AddListener(() =>
        {
            isSpawning = false;
            starSphereControl.transform.parent = gameMasterRef.CenterTransform;
        });
    }
}
