﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaterialCacheManager : MonoBehaviour
{
    [Header("Cached Object")]
    [SerializeField]
    private GameObject cachedPrefab;

    public Material this[int index]
    {
        get
        {
            var renderer = this.transform.GetChild(index).GetComponent<LineRenderer>();
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
        cache.gameObject.name += "[Cache]";

        var renderer = cache.GetComponent<LineRenderer>();
        renderer.material.SetColor(name, color);
    }
}