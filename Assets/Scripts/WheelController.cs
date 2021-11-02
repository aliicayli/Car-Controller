using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public GameObject[] wheelsToRotate;
    public float rotationSpeed;
    private Animator animator;
    public TrailRenderer[] trails;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical");
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        foreach (var wheel in wheelsToRotate)
        {
            wheel.transform.Rotate(Time.deltaTime * verticalAxis * rotationSpeed, 0, 0, Space.Self);
        }

        if (horizontalAxis > 0)
        {
            // turning right
            animator.SetBool("goingLeft", false);
            animator.SetBool("goingRight", true);
        }
        else if (horizontalAxis < 0)
        {
            // turning left
            animator.SetBool("goingRight", false);
            animator.SetBool("goingLeft", true);
        }
        else
        {
            // must be going straight
            animator.SetBool("goingRight", false);
            animator.SetBool("goingLeft", false);
        }

        if (horizontalAxis != 0)
        {
            foreach (var trail in trails)
            {
                trail.emitting = true;
            }
        }
        else
        {
            foreach (var trail in trails)
            {
                trail.emitting = false;
            }
        }
    }
}
