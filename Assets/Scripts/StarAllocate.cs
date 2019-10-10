using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAllocate : MonoBehaviour
{
    [SerializeField]
    private GameObject starPrefab;

    [SerializeField]
    private Texture2D backgroundTexture;
    [SerializeField]
    private Texture2D hightlightTexture;
    [SerializeField]
    private Material starMaterial;

    private List<GameObject> cachedStarPrefab = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    { 
        //複数種類のPrefabを作る
        for(int i = 0; i < 5; i++)
        {
            GameObject cachedPrefab = Instantiate(starPrefab);
            var starControl = cachedPrefab.GetComponent<StarControl>();
            starControl.BackgroundTexture = backgroundTexture;
            starControl.HighlightTexture = hightlightTexture;
            starControl.HighlightColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            starControl.BrightSeed = Random.Range(0, 255);

            cachedStarPrefab.Add(cachedPrefab);
        }

        //適当に選んで配置する
        for(int i = 0; i < 100; i++)
        {
            GameObject star = Instantiate(cachedStarPrefab[Random.Range(0, cachedStarPrefab.Count - 1)]);
            star.transform.localPosition = new Vector3(
                Random.Range(-10, 10),
                Random.Range(-6, 6),
                Random.Range(0,10));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
