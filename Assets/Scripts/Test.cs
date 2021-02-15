using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float mSpeed = 2f;
    public float mRotSpeed = 120;
    public float mRotation = 0f;
    public float mGravity = 8;

    private Vector3 mMoveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.transform.position.Set(1.5f, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            mMoveDirection = new Vector3(0, 0, 1);
            mMoveDirection *= mSpeed;
            mMoveDirection = transform.TransformDirection(mMoveDirection);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            mMoveDirection = new Vector3(0, 0, -1);
            mMoveDirection *= mSpeed;
            mMoveDirection = transform.TransformDirection(mMoveDirection);
        }
        //if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    ChickenStopMove();
        //}
        mRotation += Input.GetAxis("Horizontal") * mRotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mRotation, 1);

        mMoveDirection.y -= mGravity * Time.deltaTime;
    }
}
