using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    [SerializeField] private float secondsToWait = 6f;


    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(ShowCanvas());
        }
    }

    public IEnumerator ShowCanvas()
    {
        cam1.enabled = false;
        cam2.enabled = true;
        yield return new WaitForSeconds(secondsToWait);
        cam2.enabled = false;
        cam1.enabled = true;
    }
}
