using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("inTrigger");
        Destroy(other);
    }
}
