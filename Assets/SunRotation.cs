using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    void Update()
    {
        transform.localEulerAngles += new Vector3(0, 0, 1) * -30f* Time.deltaTime;
    }
}
