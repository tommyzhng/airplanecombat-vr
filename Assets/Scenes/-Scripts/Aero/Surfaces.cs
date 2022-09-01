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
    //Rotate Physics Surface
    public Transform planeSurface;
    private Quaternion physicsInitRotation;
    private Quaternion surfaceInitRotation;
    public float rotationSpeed = 90;
    //Animate Visible Surfaces
    private Vector3 axis;

    void Awake()
    {
        //initial rotation set as default spawned rotation - accessed later to default to these values when let go of keys
        physicsInitRotation = transform.localRotation;
        surfaceInitRotation = planeSurface.transform.localRotation;
        //Detect Rudder
        axis = (planeSurface.name == "rudder") ? Vector3.forward : Vector3.left;
    }
    void FixedUpdate()
    {
        desiredAngle = target * max;    //target value (decimal) times 15 degrees gives us the desired degree of the rotation
        curAngle = Mathf.MoveTowardsAngle(curAngle, desiredAngle, rotationSpeed * Time.fixedDeltaTime);

        //Rotate Physics
        transform.localRotation = physicsInitRotation;
        transform.Rotate(Vector3.right, curAngle, Space.Self);

        //Animate
        planeSurface.localRotation = surfaceInitRotation;
        planeSurface.transform.Rotate(axis, curAngle, Space.Self);
    }
}
