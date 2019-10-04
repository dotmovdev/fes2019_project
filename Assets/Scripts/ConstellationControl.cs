using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationControl : MonoBehaviour
{
    [SerializeField]
    private ConstellationLineControl constellationLine;

    // Start is called before the first frame update
    void Start()
    {
        constellationLine.LinePoints = new Line(
            new Vector3(10, 10, 10), new Vector3(30, 30, 30));
        constellationLine.DrawDuration = 5.0f;
        constellationLine.DrawLine(() =>
        {
            Debug.Log("finish to draw!");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
