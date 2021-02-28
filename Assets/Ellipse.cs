using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse {
    // Start is called before the first frame update

    public float r;
    public float yAxis;
    public float zR;
    public float xR;
    public float yR;

    public Ellipse(float r, float yAxis, float zRotation, float xRotation, float yRotation, float teta, float fi)
    {
        this.r = r;
        this.yAxis = yAxis;
        this.zR = zRotation;
        this.xR = xRotation;
        this.yR = yRotation;
    }

    public Vector2 Evaluate(float t)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * r;
        float y = Mathf.Cos(angle) * yAxis;
        return new Vector2(x, y);
    }
    
    public Vector3 Evaluate3(float t)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Cos(angle) * r;
        float y = Mathf.Sin(angle) * yAxis;
        float z = 0f;


        x = x * Mathf.Cos(Mathf.Deg2Rad * zR) * Mathf.Cos(Mathf.Deg2Rad * yR) - y * Mathf.Cos(Mathf.Deg2Rad * yR) + z * Mathf.Sin(Mathf.Deg2Rad * yR);
        y = x * (Mathf.Sin(xR * Mathf.Deg2Rad) * Mathf.Sin(yR * Mathf.Deg2Rad) * Mathf.Cos(zR * Mathf.Deg2Rad) + Mathf.Cos(xR * Mathf.Deg2Rad) * Mathf.Sin(zR * Mathf.Deg2Rad)) + y * (Mathf.Cos(xR * Mathf.Deg2Rad) * Mathf.Cos(zR * Mathf.Deg2Rad) - Mathf.Sin(xR * Mathf.Deg2Rad) * Mathf.Sin(yR * Mathf.Deg2Rad) * Mathf.Sin(zR * Mathf.Deg2Rad)) - z * (Mathf.Sin(xR * Mathf.Deg2Rad) * Mathf.Cos(yR * Mathf.Deg2Rad));
        z = x * (Mathf.Sin(xR * Mathf.Deg2Rad) * Mathf.Sin(zR * Mathf.Deg2Rad) - Mathf.Cos(xR * Mathf.Deg2Rad) * Mathf.Sin(yR * Mathf.Deg2Rad) * Mathf.Cos(zR * Mathf.Deg2Rad)) + y * (Mathf.Cos(xR * Mathf.Deg2Rad) * Mathf.Sin(yR * Mathf.Deg2Rad) * Mathf.Sin(zR * Mathf.Deg2Rad) + Mathf.Sin(xR * Mathf.Deg2Rad) * Mathf.Cos(zR * Mathf.Deg2Rad)) + z * (Mathf.Cos(xR * Mathf.Deg2Rad) * Mathf.Cos(yR * Mathf.Deg2Rad));
        return new Vector3(x, z, y);
    }

    public Vector3 Evaluate2(float t)
    {
        //float angle1 = Mathf.Deg2Rad * 360f * t;
        float angle2 = Mathf.Deg2Rad * 360f * t;
        float angle1 = Mathf.Deg2Rad * 90f;
        float x = Mathf.Sin(angle1) * Mathf.Cos(angle2) * zR;
        float y = Mathf.Sin(angle1) * Mathf.Sin(angle2) * zR;
        float z = Mathf.Cos(angle1) * zR;
        return new Vector3(x, z, y);
    }



}
