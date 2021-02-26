using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitters : MonoBehaviour
{
    public int radius = 200;
    public GameObject spaceShip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = this.transform.position - spaceShip.transform.position;

        float dist = difference.magnitude;
        Vector3 gravityDiraction = difference.normalized;
        float gravity = 6.7f * (this.transform.localScale.x * spaceShip.transform.localScale.x * 80) / (dist * dist);
        Vector3 gravityVector = (gravityDiraction * gravity);
        spaceShip.transform.GetComponent<Rigidbody>().AddForce(gravityVector, ForceMode.Acceleration);
    }
}
