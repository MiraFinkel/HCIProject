using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRenderer : MonoBehaviour
{
    [Range(3, 10)]
    [HideInInspector] public int numOfSquers = 3;
    [Range(0, 1)]
    [HideInInspector] public float scale = 1f;
    [Range(0, 1)]
    [HideInInspector] public float red;
    [Range(0, 1)]
    [HideInInspector] public float green;
    [Range(0, 1)]
    [HideInInspector] public float blue;


    private List<Vector3> verticies;
    private List<int> triangles;
    private Mesh mesh;

    private float adjScale;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjScale = scale * 0.5f;
    }

    void Start()
    {
        GenerateVoxelMesh(new VoxelData(numOfSquers));
        UpdateMesh();
    }

    void GenerateVoxelMesh(VoxelData data)
    {
        verticies = new List<Vector3>();
        triangles = new List<int>();
        for (int z = 0; z < data.Depth; z++)
        {
            for (int x = 0; x < data.Width; x++)
            {
                if (data.GetCell(x,z) == 0)
                {
                    continue;
                }
                MakeCube(adjScale, new Vector3((float)x * scale, 0, (float)z * scale), x, z, data);
            }
        }
    }

    void MakeCube(float cubeScale, Vector3 cubePos, int x, int z, VoxelData data)
    {
        for (int i = 0; i < 6; i++)
        {
            if(data.GetNeighbor(x, z, (Direction)i) == 0)
            {
                MakeFace((Direction)i, cubeScale, cubePos);
            }
        }
    }

    void MakeFace(Direction direction, float faceScale, Vector3 facePos)
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

        GetComponent<MeshRenderer>().material.SetColor("_Color", getColor());
    }

    Color getColor()
    {
        Color color = Color.white;
        color.r = red;
        color.g = green;
        color.b = blue;
        return color;
    }

}
