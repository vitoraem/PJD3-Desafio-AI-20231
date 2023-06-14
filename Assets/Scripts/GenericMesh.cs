using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMesh : MonoBehaviour
{
    public Material material;

    public List<Vector3> vertices;

    public List<Vector2> uvs;

    public List<int> triangles;

    public Mesh mesh;

    public MeshTopology topology;

    private void Awake()
    {
        GenerateQuad();
    }

    void GenerateTriangle()
    {
        GameObject go = new GameObject("Triangle");
        MeshFilter filter = go.AddComponent<MeshFilter>();
        MeshRenderer renderer = go.AddComponent<MeshRenderer>();

        renderer.sharedMaterial = material;

        mesh = new Mesh();
        mesh.name = "Triangle";

        vertices = new List<Vector3>();
        vertices.Add(new Vector3(0, 0, 0));  // 0
        vertices.Add(new Vector3(1f, 0, 0)); // 1
        vertices.Add(new Vector3(1f, 1f, 0));// 2

        triangles = new List<int>()
        {
            0,2,1,
            0,1,2
        };

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();


        filter.mesh = mesh;
    }

    void GenerateQuad()
    {
        GameObject go = new GameObject("Quad");
        MeshFilter filter = go.AddComponent<MeshFilter>();
        MeshRenderer renderer = go.AddComponent<MeshRenderer>();

        renderer.sharedMaterial = material;

        mesh = new Mesh();
        mesh.name = "Quad";

        vertices = new List<Vector3>() 
        {
            new Vector3(0, 0, 0),  // 0
            new Vector3(1f, 0, 0), // 1
            new Vector3(1f, 1f, 0),// 2
            new Vector3(0,1f,0)    // 3
        };

        uvs = new List<Vector2>()
        {
            new Vector2(0,0),
            new Vector2(1f,0),
            new Vector2(1f,1f),
            new Vector2(0,1f),
        };
        

        triangles = new List<int>()
        {
            0,2,1,
            0,3,2
        };

        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        //mesh.triangles = triangles.ToArray();
        topology = MeshTopology.Triangles;
        mesh.SetIndices(triangles.ToArray(), topology,0);

        filter.mesh = mesh;
    }

    private void Update()
    {
        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        //mesh.triangles = triangles.ToArray();
        mesh.SetIndices(triangles.ToArray(), topology, 0);
    }
}
