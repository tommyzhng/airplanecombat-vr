using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerParameters : MonoBehaviour
{
    public Rigidbody rbPlane;

    public float velocity;
    public float altitudeMSL;
    public float altitudeAGL;
    private void Awake()
    {
        rbPlane= GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Speeds();
        Altitude();
        Debug.Log(altitudeAGL + " " + altitudeMSL);
    }
    private void Speeds()
    {
        velocity = rbPlane.velocity.magnitude;
    }
    public void Altitude()  //Convert meters to feet
    {
        altitudeMSL = Mathf.Abs(Mathf.Floor (transform.position.y * 3.281f));       //Above sea level is just transform position.

        //AGL requires more calculations:
        RaycastHit hit;
        Ray rayDown = new Ray(transform.position, new Vector3(0, -1, 0));
        if (Physics.Raycast(rayDown, out hit))
        {                                               //height of wheels
            altitudeAGL = Mathf.Abs(Mathf.Floor((hit.distance - 5.715672f) * 3.281f));
        }
    }
}
