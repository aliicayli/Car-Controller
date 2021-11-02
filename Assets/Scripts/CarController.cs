using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody sphereRB;

    private float moveInput;
    private float turnInput;

    public float forwardSpeed;
    public float reverseSpeed;
    public float turnSpeed;

    private bool isCarGrounded;

    public LayerMask groundLayer;

    public float airDrag;
    public float groundDrag;

    public float alignToGroundTime;
    
    void Start()
    {
        //detach rigidbody from car
        sphereRB.transform.parent = null;
    }

    
    void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");


        //adjust speed for car
        moveInput *= moveInput >0 ? forwardSpeed : reverseSpeed;

        //if(moveInput > 0)
        //{
        //    // your going forward
        //    moveInput *= forwardSpeed;
        //}
        //else
        //{
        //    //your going backwards
        //    moveInput *= reverseSpeed;
        //}


        // set cars position to sphere
        transform.position = sphereRB.transform.position;

        // set cars rotation
        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0, Space.World);


        // raycast ground check
        RaycastHit raycastHit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out raycastHit,1f, groundLayer);


        // rotate car to be parallel to ground
        //transform.rotation = Quaternion.FromToRotation(transform.up, raycastHit.normal) * transform.rotation;
        Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, raycastHit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);




        if (isCarGrounded)
        {
            sphereRB.drag = groundDrag;
        }
        else
        {
            sphereRB.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        if (isCarGrounded)
        {
            // move car
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {
            // add extra gravity
            sphereRB.AddForce(transform.up * -30f);
        }
        
    }
}
