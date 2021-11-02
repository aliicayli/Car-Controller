using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float lerpValue;
    

    void LateUpdate()
    {
        Vector3 devPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, devPos, lerpValue);
    }
}
