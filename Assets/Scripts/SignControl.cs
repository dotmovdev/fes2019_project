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

    private static float signScale = 1.0f;
    private static GameMaster gameMasterRef;

    private List<SignLineControl> signLines = new List<SignLineControl>();
    public List<SignLineControl> SignLines
    {
        get
        {
            return signLines;
        }
    }

    public Transform SignTransform
    {
        get
        {
            return this.transform;
        }
    }

    private void Awake()
    {
        if (gameMasterRef == null)
        {
            gameMasterRef = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AllocateStars(Sign sign, Action onCompleteAction)
    {
        var starPositions = sign.starPositions;

        bool isLineSpawn = false;
        int priority = 1;

        for(int i = 0; i < starPositions.Length; i++)
        {
            var star = instantiateStar();
            star.Material = gameMasterRef.StarCache[sign.colorIndex];
            star.transform.localPosition = Vector3.zero;
            star.RenderProprity = priority;

            priority++;

            var starTween = DOTween.To(
                () => star.transform.localPosition,
                (position) => star.transform.localPosition = position,
                starPositions[i], allocateDuration);
            starTween.SetEase(easeType);

            if (!isLineSpawn)
            {
                starTween.OnComplete(() =>
                {
                    allocateLines(sign);
                    onCompleteAction.Invoke();
                });

                isLineSpawn = true;
            }
        }
    }

    private void allocateLines(Sign sign)
    {
        Debug.Log(sign.lines.Length);
        
        for(int i = 0; i < sign.lines.Length; i++)
        {
            var lineControl = instantiateLine();
            lineControl.Material = gameMasterRef.LineCache[sign.colorIndex];
            Debug.LogFormat("ColorIndex: {0}", sign.colorIndex);

            int startIndex = sign.lines[i].startIndex;
            int endIndex = sign.lines[i].endIndex;

            lineControl.LinePoints = new Line(
                sign.starPositions[startIndex], sign.starPositions[endIndex]
                );

            signLines.Add(lineControl);
        }
    }

    private StarControl instantiateStar(GameObject starPrefab)
    {
        GameObject star = Instantiate(starPrefab, SignTransform);
        return star.GetComponent<StarControl>();
    }

    private StarControl instantiateStar()
    {
        GameObject star = Instantiate(this.starPrefab, SignTransform);
        return star.GetComponent<StarControl>();
    }

    private SignLineControl instantiateLine()
    {
        GameObject line = Instantiate(signLinePrefab, SignTransform);
        return line.GetComponent<SignLineControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
