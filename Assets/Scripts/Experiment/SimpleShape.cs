using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShape
{
    private GameObject gameObject;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private Mesh mesh = new Mesh();

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> tris = new List<int>();

    public SimpleShape(GameObject gameObject)
    {
        this.gameObject = new GameObject("SimpleShape");
        this.gameObject.transform.parent = gameObject.transform;
        meshFilter = this.gameObject.AddComponent<MeshFilter>();
        meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
    }

    public void SetMaterial(Material material)
    {
        meshRenderer.material = material;
    }

    public void AddLine(Vector3 start, Vector3 end, float width)
    {
        Vector2 substract = new Vector2(end.x, end.y) - new Vector2(start.x, start.y);
        Vector2 minus90 = Quaternion.Euler(0, 0, -90) * substract.normalized;
        minus90 *= width / 2f;
        Vector2 plus90 = Quaternion.Euler(0, 0, 90) * substract.normalized;
        plus90 *= width / 2f;

        vertices.Add(start + new Vector3(minus90.x, minus90.y, 0f));
        vertices.Add(end + new Vector3(minus90.x, minus90.y, 0f));
        vertices.Add(start + new Vector3(plus90.x, plus90.y, 0f));
        vertices.Add(end + new Vector3(plus90.x, plus90.y, 0f));

        tris.Add(vertices.Count - 4);
        tris.Add(vertices.Count - 2);
        tris.Add(vertices.Count - 3);
        tris.Add(vertices.Count - 3);
        tris.Add(vertices.Count - 2);
        tris.Add(vertices.Count - 1);

        mesh.vertices = vertices.ToArray();
        mesh.triangles = tris.ToArray();

        meshFilter.sharedMesh = mesh;
    }

    public void AddRectangle(Vector3 index, float width, float height)
    {

    }

    public void AddRectangle(Vector3 leftTop, Vector3 rightBottom)
    {

    }

    public void AddCircle(Vector3 center, float radius, int resolution)
    {

    }

    public void Clear()
    {
        mesh.Clear();

        vertices.Clear();
        tris.Clear();
    }

    public void Flush()
    {
        mesh.Clear();
        mesh = null;

        Object.DestroyImmediate(meshFilter);
        Object.DestroyImmediate(meshRenderer);
    }
}
