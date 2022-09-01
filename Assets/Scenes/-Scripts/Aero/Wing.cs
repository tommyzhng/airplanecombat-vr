using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    private Rigidbody rbPlane;
    //Wing Characteristics
    public float wingX = 3;
    public float wingY = 1.5f;
    public float liftFactor = 1f;
    public float dragFactor = 1f;

    private float lift;
    private float drag;
    public CoefficientGraph coefficients;
    //Parameters
    private float angleOfAttack;
    public bool centerForce;
    private Vector3 flowVelocity;
    private Vector3 forcePoint;

    void Awake()
    {
        rbPlane = GetComponentInParent<Rigidbody>(); //Set Rigidbody
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Call the function
        CalculateLiftAndDrag();
    }
    void CalculateLiftAndDrag()
    {
        //Angle of attack for Coefficients
        angleOfAttack = Vector3.Angle(Vector3.forward, flowVelocity);

        //Coefficients
        float liftCoefficient = coefficients.lift_Coefficient.Evaluate(angleOfAttack); //Evaluates value of Y at X on the coeffiecients graph script
        float dragCoefficient = coefficients.drag_Coefficient.Evaluate(angleOfAttack);

        //Calculate the velocity  flow over wing
        flowVelocity = transform.InverseTransformDirection(rbPlane.GetPointVelocity(transform.position));
        flowVelocity.x = 0f;

        //Calculate Lift and Drag Forces        
        //Lift/drag = 1/2pv^2 * wing area * coefficient           -density around 1.2kg/m3 at STP
        lift =  0.5f * 1.2f * flowVelocity.sqrMagnitude * (wingX * wingY) * liftCoefficient * liftFactor * Mathf.Sign(-flowVelocity.y);
        drag = 0.5f * 1.2f * flowVelocity.sqrMagnitude * (wingX * wingY) * dragCoefficient * dragFactor;

        //find where to apply the force
        forcePoint = centerForce ? rbPlane.transform.TransformPoint(rbPlane.centerOfMass) : transform.position;
        //Add force                           //lift must be in the up direction (crossing fwd & right does this)
        rbPlane.AddForceAtPosition(lift * Vector3.Cross(rbPlane.velocity, transform.right).normalized, forcePoint);
        rbPlane.AddForceAtPosition(drag * -rbPlane.velocity.normalized, forcePoint);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Matrix4x4 defaultMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(wingX, 0f, wingY));
        Gizmos.matrix = defaultMatrix;
    }
#endif
}
