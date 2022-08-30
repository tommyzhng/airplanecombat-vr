using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfaces : MonoBehaviour
{
    //Required parameters
    public float target;
    private float curAngle;
    private float desiredAngle;
    public float max = 15f;
    //Animate Surfaces
    public Transform planeSurface;
    private Quaternion physicsInitRotation;
    private Quaternion surfaceInitRotation;
    public float rotationSpeed = 90;
    void Awake()
    {
        //initial rotation set as default spawned rotation - accessed later to default to these values when let go of keys
        physicsInitRotation = transform.localRotation;
        surfaceInitRotation = planeSurface.localRotation;
    }

    void FixedUpdate()
    {
        desiredAngle = target * max;    //target value (decimal) times 15 degrees gives us the desired degree of the rotation
        curAngle = Mathf.MoveTowardsAngle(curAngle, desiredAngle, rotationSpeed * Time.fixedDeltaTime);

        //Physics
        transform.localRotation = physicsInitRotation;
        transform.Rotate(Vector3.right, curAngle, Space.Self);

        //Animate
        planeSurface.localRotation = surfaceInitRotation;

        if (planeSurface.name != "rudder")
        {
            planeSurface.Rotate(Vector3.right, curAngle, Space.Self);
        }
        else
        {
            planeSurface.Rotate(Vector3.forward, curAngle, Space.Self);
        }

    }
}
