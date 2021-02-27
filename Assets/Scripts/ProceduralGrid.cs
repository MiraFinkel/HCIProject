using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{
    [SerializeField] private Vector3[] verticies;
    [SerializeField] private int[] triangles;
    private Mesh mesh;

    //grid settings
    public float cellSize = 1;
    public Vector3 gridOffset;
    public int gridSize;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Start()
    {
        MakeContinuesProceduralGrid();
        UpdateMesh();
    }

    void MakeDiscreteProceduralGrid()
    {
        verticies = new Vector3[gridSize * gridSize * 4];
        triangles = new int[gridSize * gridSize * 6];

        int v = 0;
        int t = 0;

        float vertexOffset = cellSize * 0.5f;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 cellOffset = new Vector3(x * cellSize, 0, y * cellSize);

                verticies[v + 0] = new Vector3(-vertexOffset, x + y, -vertexOffset) + cellOffset + gridOffset;
                verticies[v + 1] = new Vector3(-vertexOffset, x + y,  vertexOffset) + cellOffset + gridOffset;
                verticies[v + 2] = new Vector3( vertexOffset, x + y, -vertexOffset) + cellOffset + gridOffset;
                verticies[v + 3] = new Vector3( vertexOffset, x + y,  vertexOffset) + cellOffset + gridOffset;

                triangles[t + 0] = v + 0;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + 2;
                triangles[t + 5] = v + 3;

                v += 4;
                t += 6;
            }
        }
    }

    void MakeContinuesProceduralGrid()
    {
        verticies = new Vector3[(gridSize + 1) * (gridSize + 1)];
        triangles = new int[gridSize * gridSize * 6];

        int v = 0;
        int t = 0;

        float vertexOffset = cellSize * 0.5f;

        //create vertex grid
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                verticies[v] = new Vector3((x * cellSize) - vertexOffset, (x + y) * 0.2f, (y * cellSize) - vertexOffset);
                v++;
            }
        }

        v = 0;
        //setting each cells triangles
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                triangles[t + 0] = v + 0;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + (gridSize + 1);
                triangles[t + 5] = v + (gridSize + 1) + 1;
                v++;
                t += 6;
            }
            v++;
        }
    }
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
