using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [Range(3, 35)]
    public int numOfSquers;

    [SerializeField] private Transform generatorPosition;
    [SerializeField] private float respawnTime = 1.0f;
    [SerializeField] private bool spawn = false;
    [SerializeField] private Color color = Color.red;



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
        shapeGO.GetComponent<VoxelRenderer>().setColor(color);
        shapeGO.transform.position = generatorPosition.position;
    }
}
