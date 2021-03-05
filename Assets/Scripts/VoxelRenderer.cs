using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRenderer : MonoBehaviour
{
    public Color color;
    [SerializeField] public bool isItSun = false;
    [SerializeField] public bool isItSpaceship = false;
    [SerializeField] public bool theEndOfTheGame = false;


    [HideInInspector] public Slider slider;
    [HideInInspector] public int numOfSquers;
    [Range(0f, 1f)]
    [HideInInspector] public float scale;

    private MeshRenderer mR;
    private BoxCollider bC;
    private List<Vector3> verticies;
    private List<int> triangles;
    private Mesh mesh;
    private float adjScale;

    void Awake()
    {
        slider = GameObject.FindObjectOfType<Slider>();
        
        numOfSquers = GameObject.FindObjectOfType<GameController>().numOfSquares;
        scale = 0.05f;
        adjScale = scale * 0.5f;

        mesh = GetComponent<MeshFilter>().mesh;
        mR = GetComponent<MeshRenderer>();
        if(!isItSpaceship && !isItSun)
        {
            bC = GetComponent<BoxCollider>();
            bC.enabled = false;
        }
        else if(isItSun)
        {
            if (bC != null) Destroy(bC);
        }
    }

    void Start()
    {
        if(isItSun)
        {
            mR.material.SetColor("_Color", Color.white);
            mR.material.DisableKeyword("_EMISSION");
        }
        else if(isItSpaceship)
        {
            mR.material.SetColor("_Color", Color.white);
            mR.material.SetColor("_EmissionColor", Color.white);
            mR.material.EnableKeyword("_EMISSION");
            transform.localEulerAngles += new Vector3(0, 0, 1) * 90f;
        }
        else
        {
            mR.material.SetColor("_Color", color);
            mR.material.SetColor("_EmissionColor", color);
            mR.material.EnableKeyword("_EMISSION");
        }
        GenerateVoxel3Mesh(new VoxelData(numOfSquers, isItSun, isItSpaceship));
        UpdateMesh();
    }

    void Update()
    {
        if (!isItSun && !isItSpaceship && !theEndOfTheGame)
        {
            transform.localEulerAngles += new Vector3(0, 0, 1) * Random.Range(80f, 100f) * Time.deltaTime;

        }
        if(theEndOfTheGame)
        {
            transform.position += transform.forward * 10f * Time.deltaTime;
        }
    }

    void GenerateVoxel2Mesh(VoxelData data)
    {
        verticies = new List<Vector3>();
        triangles = new List<int>();
        for (int z = 0; z < data.Depth; z++)
        {
            for (int x = 0; x < data.Width; x++)
            {
                if (data.Get2Cell(x,z) == 0)
                {
                    continue;
                }
                Make2Cube(adjScale, new Vector3((float)x * scale, 0, (float)z * scale), x, z, data);
            }
        }
    }

    void GenerateVoxel3Mesh(VoxelData data)
    {
        verticies = new List<Vector3>();
        triangles = new List<int>();
        for (int z = 0; z < data.Depth; z++)
        {
            for (int x = 0; x < data.Width; x++)
            {
                for (int y = 0; y < data.Height; y++)
                {
                    if (data.Get3Cell(x, y, z) == 0)
                    {
                        continue;
                    }
                    Make3Cube(adjScale, new Vector3((float)x * scale, (float)y * scale, (float)z * scale), x, y, z, data);
                }
            }
        }
    }

    void Make2Cube(float cubeScale, Vector3 cubePos, int x, int z, VoxelData data)
    {
        for (int i = 0; i < 6; i++)
        {
            if(data.Get2Neighbor(x, z, (Direction)i) == 0)
            {
                MakeFace((Direction)i, cubeScale, cubePos);
            }
        }
    }

    void Make3Cube(float cubeScale, Vector3 cubePos, int x, int y, int z, VoxelData data)
    {
        for (int i = 0; i < 6; i++)
        {
            if (data.Get3Neighbor(x, y, z, (Direction)i) == 0)
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
    }

    void UpdateRandomColor(Color colorToUpdate)
    {
        setRandomColor();
        mR.material.SetColor("_Color", colorToUpdate);
        mR.material.SetColor("_EMISSION", colorToUpdate);
        mR.material.EnableKeyword("_EMISSION");
    }

    void setRandomColor()
    {
        color.r = Random.Range(0f, 1f);
        color.g = Random.Range(0f, 1f);
        color.b = Random.Range(0f, 1f);
    }

    public void setColor(Color curColor)
    {
        color = curColor;
    }

    public void MoveTowordsTheSun()
    {
        GameObject sun =  GameObject.FindGameObjectWithTag("Sun");
        transform.LookAt(sun.transform);
        bC.enabled = true;
    }

}
