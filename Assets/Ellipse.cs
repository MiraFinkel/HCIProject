using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse {
    // Start is called before the first frame update

    public float r;
    public float yAxis;
    public float zR = 0;
    public float xR = 0;
    public float yR = 0;

    public Ellipse(float r, float yAxis, float zRotation, float xRotation, float yRotation, float teta, float fi)
    {
        this.r = r;
        this.yAxis = yAxis;
    }

    public Vector2 Evaluate(float t)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * r;
        float y = Mathf.Cos(angle) * yAxis;
        return new Vector2(x, y);
    }
    
    public Vector3 Evaluate3D(float t)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Cos(angle) * r;
        float y = Mathf.Sin(angle) * yAxis;


        float x_t = x * Mathf.Cos(zR) * Mathf.Cos(yR) - y * Mathf.Cos(yR);
        float y_t = x * (Mathf.Sin(xR) * Mathf.Sin(yR) * Mathf.Cos(zR) + Mathf.Cos(xR) * Mathf.Sin(zR)) + y * (Mathf.Cos(xR) * Mathf.Cos(zR) - Mathf.Sin(xR) * Mathf.Sin(yR) * Mathf.Sin(zR));
        float z_t = x * (Mathf.Sin(xR) * Mathf.Sin(zR) - Mathf.Cos(xR) * Mathf.Sin(yR) * Mathf.Cos(zR)) + y * (Mathf.Cos(xR) * Mathf.Sin(yR) * Mathf.Sin(zR) + Mathf.Sin(xR) * Mathf.Cos(zR));
        return new Vector3(x_t, z_t, y_t);
    }
}
