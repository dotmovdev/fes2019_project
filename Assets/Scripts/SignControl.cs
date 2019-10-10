using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SignExtensions;
using DG.Tweening;

public class SignControl : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField]
    private GameObject starPrefab;
    [SerializeField]
    private GameObject signLinePrefab;

    [Header("Animation Tween")]
    [SerializeField]
    private float allocateDuration = 1.0f;
    [SerializeField]
    private Ease easeType;

    private Transform signTransform
    {
        get
        {
            return this.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //signLine.LinePoints = new Line(
        //    new Vector3(10, 10, 10), new Vector3(30, 30, 30));
        //signLine.DrawDuration = 5.0f;
        //signLine.DrawLine(() =>
        //{
        //    Debug.Log("finish to draw!");
        //});

    }

    public void AllocateStars(Vector3[] starPositions, Action onCompleteAction)
    {
        foreach(var starPosition in starPositions)
        {
            var star = instantiateStar();
            star.transform.localPosition = Vector3.zero;

            var starTween = DOTween.To(
                () => star.transform.localPosition,
                (position) => star.transform.localPosition = position,
                starPosition, allocateDuration);
            starTween.SetEase(easeType);
        }

        onCompleteAction.Invoke();
    }

    private StarControl instantiateStar(GameObject starPrefab)
    {
        GameObject star = Instantiate(starPrefab, signTransform);
        return star.GetComponent<StarControl>();
    }

    private StarControl instantiateStar()
    {
        GameObject star = Instantiate(this.starPrefab, signTransform);
        return star.GetComponent<StarControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
