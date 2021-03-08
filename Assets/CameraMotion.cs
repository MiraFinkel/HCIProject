using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Input;

public class CameraMotion : MonoBehaviour
{
    public Transform orbittinObject;
    public Transform gravityCenter;
    public float hight = 8;

    [Range(0f, 1f)]
    public float orbitProgrss = 0f;
    public float orbitPeriod = 72f;
    public bool orbitActive = true;

    void Start()
    {
        if (orbittinObject == null)
        {
            orbitActive = false;
            return;
        }

        StartCoroutine(AnimateOrbit());
    }

    private void Update()
    {
        transform.LookAt(gravityCenter.position);
        SetOrbitingObjectPositiion();
    }

    void SetOrbitingObjectPositiion()
    {
        float angle = Mathf.Deg2Rad * 360f * orbitProgrss;
        float temp = 20;
        float x = Mathf.Cos(angle) * temp;
        float y = Mathf.Sin(angle) * temp;
        orbittinObject.localPosition = new Vector3(x, 8 ,y) + gravityCenter.transform.position;
    }

    IEnumerator AnimateOrbit()
    {

        float orbitSpeed = 1f / 72f;

        while (orbitActive)
        {
            orbitProgrss += Time.deltaTime * orbitSpeed;
            orbitProgrss %= 1f;
            SetOrbitingObjectPositiion();
            yield return null;
        }
    }

}