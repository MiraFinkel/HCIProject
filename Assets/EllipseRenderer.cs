﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour
{
    LineRenderer lr;

    [Range(3, 36)]
    public int segments;
    public Ellipse ellipse;

    void Awake()
    {
        lr = GetComponent<LineRenderer> ();
        CalculateEllipse();
    }

    void CalculateEllipse()
    {
        if (lr == null)
        {
            lr = GetComponent<LineRenderer>();
        }
        Vector3[] points = new Vector3[segments + 1];
        for (int i =0; i < segments; i++)
        {
            //Vector2 position2D = ellipse.Evaluate((((float)i / (float)segments)));
            points[i] = ellipse.Evaluate3D((((float)i / (float)segments)));
        }
        points[segments] = points[0];

        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    void OnValidate()
    {
        CalculateEllipse();
    }
}
