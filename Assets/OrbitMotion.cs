using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Input;

[RequireComponent(typeof(LineRenderer))]
public class OrbitMotion : MonoBehaviour
{
    public Transform orbittinObject;
    public Ellipse orbitPath;
    public GameObject gravityCenter;

    LineRenderer lr;
    [Range(3, 36)]
    public int segments;

    [Range(0f,1f)]
    public float orbitProgrss = 0f;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;

    private float cooldown = 0f;
    private float orbitRate = 3f;

    void Start()
    {
        if (orbittinObject == null)
        {
            orbitActive = false;
            return;
        }
        lr = GetComponent<LineRenderer>();
        CalculateEllipse();
        SetOrbitingObjectPositiion();
        StartCoroutine(AnimateOrbit());

    }


    void CalculateEllipse()
    {
        if (lr == null)
        {
            lr = GetComponent<LineRenderer>();
        }
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i < segments; i++)
        {
            points[i] = orbitPath.Evaluate3D((((float)i / (float)segments))) + gravityCenter.transform.position;
        }
        points[segments] = points[0];

        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    public void AxisRotationUpdate(float key)
    {
        if (key == 2f && Time.time > cooldown)
        {
            cooldown = Time.time + orbitRate;
            orbitPath.xR += (22.5f * Mathf.Deg2Rad);
            if (orbitPath.xR == (360f * Mathf.Deg2Rad))
            {
                orbitPath.xR = (0f * Mathf.Deg2Rad);
            }
            CalculateEllipse();
        }
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > cooldown)
        {
            cooldown = Time.time + orbitRate;
            orbitPath.zR += (22.5f * Mathf.Deg2Rad);
            if (orbitPath.zR == (360f * Mathf.Deg2Rad))
            {
                orbitPath.zR = (0f * Mathf.Deg2Rad);
            }
            CalculateEllipse();
        }
        if (key == 3f && Time.time > cooldown)
        {
            cooldown = Time.time + orbitRate;
            orbitPath.yR += (22.5f * Mathf.Deg2Rad);
            if (orbitPath.yR == (360f * Mathf.Deg2Rad))
            {
                orbitPath.yR = (0f * Mathf.Deg2Rad);
            }
            CalculateEllipse();
           
        }
    }

    void SetOrbitingObjectPositiion()
    {
        //AxisRotationUpdate();
        orbittinObject.localPosition = orbitPath.Evaluate3D(orbitProgrss);
    }

    IEnumerator AnimateOrbit()
    {
        if (orbitPeriod < 0.1f)
        {
            orbitPeriod = 0.1f;
        }
        float orbitSpeed = 1f / orbitPeriod;

        while (orbitActive)
        {
            orbitProgrss += Time.deltaTime * orbitSpeed;
            orbitProgrss %= 1f;
            SetOrbitingObjectPositiion();
            yield return null;
        }
    }

}
