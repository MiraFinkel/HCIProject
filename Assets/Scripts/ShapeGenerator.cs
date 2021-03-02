using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] private Transform generatorPosition;
    [SerializeField] private OrbitMotion orbitMotion;
    //[SerializeField] private int shapeAmount = 1;
    [SerializeField] private float respawnTime = 1.0f;
    [SerializeField] private bool spawn = false;


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
        //shapeGO.transform.position = orbitMotion.orbittinObject.localPosition;
    }
}
