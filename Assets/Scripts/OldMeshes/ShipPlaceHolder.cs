using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipPlaceHolder : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;

    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;


    void Update()
    {

        activeForwardSpeed = Input.GetAxisRaw("Vertical") * forwardSpeed;
        activeStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        activeHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;

        transform.position += transform.up * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.forward * activeHoverSpeed * Time.deltaTime;

    }
}
