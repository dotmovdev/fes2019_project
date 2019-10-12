using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMassControl : MonoBehaviour
{
    [SerializeField]
    private GameObject starMassElementPrefab;

    [SerializeField]
    private List<StarMassElementControl> massElementControlRefs = new List<StarMassElementControl>();

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
        
    }
}
