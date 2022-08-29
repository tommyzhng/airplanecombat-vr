using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrottleRotation : MonoBehaviour
{
    Vector3 curRotation;
    Vector3 targetRotation;
    float throttle = 0f;
    float throttleChange;

    //Oculus Control
    private bool lPressed;
    private Collider enteredTrigger;

    private void Start()
    {
        curRotation = transform.localEulerAngles;
        targetRotation = new Vector3(curRotation.x, 270, curRotation.z) ;
    }
    private void Update()
    {
        //Rotate On Keyboard Input
        if (throttleChange == -1 || throttleChange == 1) 
        {
            throttle += throttleChange * Time.deltaTime;
            throttle = Mathf.Clamp01(throttle);
            transform.localEulerAngles = Vector3.Lerp(curRotation, targetRotation, throttle);
        }
        //Oculus
        if (lPressed & enteredTrigger) { RotateJoystick(); }
        else if (!lPressed & enteredTrigger) { enteredTrigger = null; }
    }
    //Oculus Control
    private void RotateJoystick()
    {
        Vector3 direction = transform.position - enteredTrigger.transform.position;
        Vector3 angle = Quaternion.LookRotation(direction).eulerAngles;
        transform.rotation = Quaternion.Euler(angle);
        transform.localEulerAngles = new Vector3(283.85f,Mathf.Clamp(transform.localEulerAngles.y, 170, 270), 145.69f);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LHand"))
        {
            enteredTrigger = other;
        }
    }
    public void LGrab(InputAction.CallbackContext leftGripPressed)
    {
        lPressed = leftGripPressed.ReadValueAsButton();
    }

    //Keyboard Control
    public void Throttle(InputAction.CallbackContext throttle)
    {
        throttleChange = throttle.ReadValue<float>();
    }

    
}

