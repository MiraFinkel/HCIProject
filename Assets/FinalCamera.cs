using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCamera : MonoBehaviour
{
    public Transform gravityCenter;
    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    public bool orbitActive = true;
    Transform mTransform;

    void Start()
    {
        mTransform = transform;
        //StartCoroutine(AnimateOrbit());
    }

    private void Update()
    {
        //transform.LookAt(gravityCenter.position);
        //SetOrbitingObjectPositiion();
        StartCoroutine(CameraHover());
    }

    IEnumerator CameraHover()
    {
        mTransform.position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(1f);
        mTransform.position -= new Vector3(0, 0.1f, 0);

    }

    void SetOrbitingObjectPositiion()
    {
        float angle = Mathf.Deg2Rad * 360f * orbitProgress;
        float temp = 20;
        float x = Mathf.Cos(angle) * temp;
        float y = Mathf.Sin(angle) * temp;
        transform.localPosition = new Vector3(x, 8, y) + gravityCenter.transform.position;
    }

    IEnumerator AnimateOrbit()
    {

        float orbitSpeed = 1f / 72f;

        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjectPositiion();
            yield return null;
        }
    }
}
