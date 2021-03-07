using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.CompareTag("Sun"))
    //    {
    //        return;
    //    }
    //    //other.gameObject.SetActive(false);
    //    Destroy(other.gameObject);
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sun"))
        {
            return;
        }
        Destroy(other.gameObject);
    }
}
