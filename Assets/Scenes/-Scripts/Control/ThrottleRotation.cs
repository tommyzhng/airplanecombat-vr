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

    private void Start()
    {
        curRotation = transform.localEulerAngles;
        targetRotation = new Vector3(curRotation.x, 270, curRotation.z) ;
    }
    private void Update()
    {
        if (throttleChange == -1 || throttleChange == 1) 
        {
            throttle += throttleChange * Time.deltaTime;
            throttle = Mathf.Clamp01(throttle);
            transform.localEulerAngles = Vector3.Lerp(curRotation, targetRotation, throttle);
        }
    }
    public void Throttle(InputAction.CallbackContext throttle)
    {
        throttleChange = throttle.ReadValue<float>();
    }
}

