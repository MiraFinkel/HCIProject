using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] private Transform generatorPosition;
    //[SerializeField] private int shapeAmount = 1;
    [SerializeField] private float respawnTime = 1.0f;
    [SerializeField] private bool spawn = false;

    //[Range(3, 10)]
    //[SerializeField] private int numOfSquers = 3;
    //[Range(0f, 1f)]
    //[SerializeField] private float scale = 1f;
    //[Range(0f, 1f)]
    //[SerializeField] private float red;
    //[Range(0f, 1f)]
    //[SerializeField] private float green;
    //[Range(0f, 1f)]
    //[SerializeField] private float blue;

    void Start()
    {
        StartCoroutine(GenerateShapes());
    }

    IEnumerator GenerateShapes()
    {
        while(spawn)
        {
            yield return new WaitForSeconds(respawnTime);
            GenerateShape();
        }
        
    }

    private void  GenerateShape()
    {
        GameObject shapeGO = Instantiate(Resources.Load("Shape")) as GameObject;
        shapeGO.transform.position = generatorPosition.position;

        VoxelRenderer voxelRenderer = shapeGO.GetComponent<VoxelRenderer>();

        voxelRenderer.numOfSquers = Random.Range(3, 10);
        voxelRenderer.scale = Random.Range(0f, 1f);
        voxelRenderer.red = Random.Range(0f, 1f);
        voxelRenderer.green = Random.Range(0f, 1f);
        voxelRenderer.blue = Random.Range(0f, 1f);
    }
}
