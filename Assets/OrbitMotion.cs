using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    public Transform orbittinObject;
    public Ellipse orbitPath;

    [Range(0f,1f)]
    public float orbitProgrss = 0f;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;

    void Start()
    {
        if (orbittinObject == null)
        {
            orbitActive = false;
            return;
        }
        SetOrbitingObjectPositiion();
        StartCoroutine(AnimateOrbit());

    }

    void SetOrbitingObjectPositiion()
    {
        
        //Vector2 orbitPos = orbitPath.Evaluate(orbitProgrss);
        //orbittinObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
        
        orbittinObject.localPosition = orbitPath.Evaluate3(orbitProgrss);


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
