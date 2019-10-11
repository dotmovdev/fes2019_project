using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCacheManager : MonoBehaviour
{
    [Header("Cached Object")]
    [SerializeField]
    private GameObject cachedPrefab;

    public Material this[int index]
    {
        get
        {
            var renderer = this.transform.GetChild(index).GetComponent<MeshRenderer>();
            return renderer.sharedMaterial;
        }
    }

    public int Count
    {
        get
        {
            return this.transform.childCount;
        }
    }
    
    public void Add(string name, Color color)
    {
        GameObject cache = Instantiate(cachedPrefab, this.transform);

        var renderer = cache.GetComponent<MeshRenderer>();
        renderer.material.SetColor(name, color);
    }
}
