using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] private Transform generatorPosition;
    [SerializeField] private int shapeAmount = 1;

    [Range(3, 10)]
    [SerializeField] private int numOfSquers = 3;
    [Range(0, 1)]
    [SerializeField] private float scale = 1f;
    [Range(0, 1)]
    [SerializeField] private float red;
    [Range(0, 1)]
    [SerializeField] private float green;
    [Range(0, 1)]
    [SerializeField] private float blue;

    void Start()
    {
        for (int i = 0; i < shapeAmount; i++)
        {
            GenerateShape();
        }
    }

    // Update is called once per frame
    void GenerateShape()
    {
        GameObject shapeGO = Instantiate(Resources.Load("Shape")) as GameObject;
        shapeGO.transform.position = generatorPosition.position;
        VoxelRenderer voxelRenderer = shapeGO.GetComponent<VoxelRenderer>();
        voxelRenderer.numOfSquers = numOfSquers;
        voxelRenderer.scale = scale;
        voxelRenderer.red = red;
        voxelRenderer.green = green;
        voxelRenderer.blue = blue;
    }
}
