using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] private Transform generatorPosition;
    [HideInInspector] public float respawnTime = 1.0f;
    [HideInInspector] public bool spawn = false;
    [HideInInspector] public Color color = Color.red;



    void Update()
    {
        if (spawn)
        {
            StartCoroutine(GenerateShapes());
        }
    }

    IEnumerator GenerateShapes()
    {
        yield return new WaitForSeconds(respawnTime);
        GenerateShape();
    }

    private void  GenerateShape()
    {
        GameObject shapeGO = Instantiate(Resources.Load("Shape")) as GameObject;
        shapeGO.GetComponent<VoxelRenderer>().setColor(color);
        shapeGO.transform.position = generatorPosition.position;
    }
}
