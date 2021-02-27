using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralCube : MonoBehaviour
{
    [SerializeField] private List<Vector3> verticies;
    [SerializeField] private List<int> triangles;
    [SerializeField] private float scale = 1f;

    //grid settings
    [SerializeField] private Vector3 gridOffset;
    [SerializeField] private int gridSize;
    [SerializeField] private int posX = 1;
    [SerializeField] private int posY = 2;
    [SerializeField] private int posZ = 3;

    private Mesh mesh;

    private float adjScale;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjScale = scale * 0.5f;
    }

    void Start()
    {
        MakeCube(adjScale, new Vector3((float)posX * scale, (float)posY * scale, (float)posZ * scale));
        UpdateMesh();
    }

    void MakeCube(float cubeScale, Vector3 cubePos)
    {
        verticies = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 6; i++)
        {
            MakeFace(i, cubeScale, cubePos);
        }
    }

    void MakeFace(int direction, float faceScale, Vector3 facePos)
    {
        verticies.AddRange(CubeMeshData.FaceVertices(direction, faceScale, facePos));
        int vCount = verticies.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 1);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4 + 3);
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticies.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }


}
