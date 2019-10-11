using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public struct Line
{
    public Vector3 Start;
    public Vector3 End;

    public Line(Vector3 start, Vector3 end)
    {
        this.Start = start;
        this.End = end;
    }
}

public class SignLineControl : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    public float DrawDuration = 1.0f;

    private Line _linePoints;
    public Line LinePoints
    {
        set
        {
            Debug.Log(lineRenderer.positionCount);
            for(int i = 0; i < lineRenderer.positionCount; i++)
            {
                lineRenderer.SetPosition(i, value.Start);
            }
            endPointPosition = value.Start;
            _linePoints = value;
        }

        get
        {
            return _linePoints;
        }
    }

    public float Width
    {
        set
        {
            lineRenderer.startWidth = value;
            lineRenderer.endWidth = value;
        }
    }

    private Vector3 endPointPosition = new Vector3(0, 0, 0);

    //すべて座標を0に置き換える。
    public void InitLineRenderer()
    {
        lineRenderer.positionCount = 2;
        for(int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(0,0,0));
        }

        endPointPosition = new Vector3(0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPositions(new Vector3[]
        {
            LinePoints.Start,
            endPointPosition
        });
    }

    public void DrawLine(System.Action onCompleteAction)
    {
        var tween = DOTween.To(
            () => LinePoints.Start,
            (pnt) => endPointPosition = pnt,
            LinePoints.End,
            DrawDuration);
        tween.OnComplete(onCompleteAction.Invoke);
    }
}
