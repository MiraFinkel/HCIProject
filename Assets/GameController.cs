﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Range(3, 25)]
    [SerializeField] public float r = 5;
    [Range(0f, 1f)]
    [SerializeField] public float colorNum = 0.2f;
    [Range(1, 36)]
    [SerializeField] public int numOfSquares = 1;
    //[Range(1, 4)]
    //[SerializeField] private int span = 1;
    [Range(0.2f, 1f)]
    [SerializeField] private float respawnTime = 1f;
    [SerializeField] private bool spawn = false;

    [SerializeField] private OrbitMotion orbitMotion;
    [SerializeField] private ShapeGenerator shapeGenerator;
    [SerializeField] private InputManager inputManager;

    private Color color = Color.red;
    private bool endOfGame = false;



    void Update()
    {

        if (Input.GetKey("space"))
        {
            spawn = false;
            shapeGenerator.spawn = false;
            ExplodeAndPaint();
        }
        if (!endOfGame)
        {
            Vector4 data = inputManager.GetData();
            orbitMotion.orbitPath.r = r;
            if (data[0] == 1f)
            {
                spawn = true;
            }
            else
            {
                spawn = true;
            }
            shapeGenerator.spawn = spawn;

            colorNum = data[1] / 360;
            UpdateColor();
            shapeGenerator.color = color;

            numOfSquares = (int)(data[2] / 36) + 1;
            shapeGenerator.respawnTime = (data[3] / 450) + 0.2f;


        }
        else
        {
            shapeGenerator.spawn = false;
        }
        
    }

    void UpdateColor()
    {
        color = Color.HSVToRGB(colorNum, 1f, 1f);
    }

    void ExplodeAndPaint()
    {
        GameObject[] allShapes = GameObject.FindGameObjectsWithTag("Shape");
        foreach (var shape in allShapes)
        {
            if(shape != null)
            {
                VoxelRenderer vR = shape.GetComponent<VoxelRenderer>();
                vR.MoveTowordsTheSun();
                vR.theEndOfTheGame = true;
            }
        }
    }
}
