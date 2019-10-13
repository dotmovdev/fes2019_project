using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarMassControl : MonoBehaviour
{
    [SerializeField]
    private GameMaster gameMasterRef;
    [SerializeField]
    private Transform centralStarsVFXTransform;

    [SerializeField]
    private GameObject starMassElementPrefab;

    [SerializeField]
    private List<StarMassElementControl> massElementControlRefs = new List<StarMassElementControl>();

    private bool burstStars = false;

    public int SignCount
    {
        get
        {
            int count = 0;

            foreach(var element in massElementControlRefs)
            {
                var childSigns = element.GetComponentsInChildren<StarSphereControl>();
                count += childSigns.Length;
            }

            return count;
        }
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject starMassElement = Instantiate(starMassElementPrefab, this.transform);
            starMassElement.transform.position = Vector3.zero;

            var controlRef = starMassElement.GetComponent<StarMassElementControl>();
            massElementControlRefs.Add(controlRef);
        }
    }

    private void Update()
    {
        this.transform.localPosition = new Vector3(0, 0, 0);

        if(burstStars == false && SignCount > 2)
        {
            var spheres = SetStarMassTransform();

            bool addCallback = false;
            foreach(var sphere in spheres)
            {
                var tween = DOTween.To(
                    () => sphere.transform.localPosition,
                    (position) => sphere.transform.localPosition = position,
                    new Vector3(0, 0, 0),
                    1.0f);

                if(addCallback == false)
                {
                    tween.OnComplete(() =>
                    {
                        //いろんな方向へ飛ばす
                        foreach(var _sphere in spheres)
                        {
                            //ランダムなベクトルを作る
                            Vector3 addVector = getRandomDirection();
                            _sphere.OnUpdateEvent.AddListener(() =>
                            {
                                _sphere.transform.localPosition += addVector;
                            });
                        }
                    });
                    addCallback = true;
                }
            }

            burstStars = true;
        }
    }

    private List<StarSphereControl> SetStarMassTransform()
    {
        var controls = new List<StarSphereControl>();

        foreach (var element in massElementControlRefs)
        {
            var childSigns = element.GetComponentsInChildren<StarSphereControl>();

            foreach (var childSign in childSigns)
            {
                childSign.transform.parent = centralStarsVFXTransform;
                controls.Add(childSign);
            }
        }

        return controls;
    }

    private Vector3 getRandomDirection()
    {
        var v = Vector3.zero;

        for(int i = 0; i < 3; i++)
        {
            int randomNum = Random.Range(0, 11);
            bool isMinus = randomNum < 5 ? true : false;
            v[i] = Random.Range(0.75f, 1.5f) * (isMinus ? -1 : 1);
        }

        return v;
    }
}
