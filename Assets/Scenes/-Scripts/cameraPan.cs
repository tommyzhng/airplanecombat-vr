using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class cameraPan : MonoBehaviour
{
    [SerializeField] float sensX = 0;
    [SerializeField] float sensY = 0;
    public GameObject Cam;
    Vector2 delta;
    Vector2 currentRot;
    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            Cam.transform.localRotation = Quaternion.Euler(-currentRot.y, currentRot.x, 0);
        }
        else if (Cam.transform.localRotation != Quaternion.Euler(0, 0, 0)) 
        {
            Cam.transform.localRotation = Quaternion.Slerp(Cam.transform.localRotation, Quaternion.Euler(0f, 0f, 0), 2f * Time.deltaTime);
            currentRot.x = 0;
            currentRot.y = 0;
        }
    }
    public void panCamera(InputAction.CallbackContext mouseDelta)
    {
        if (Input.GetKey(KeyCode.V))
        {
            Cursor.lockState = CursorLockMode.Locked;
            delta = mouseDelta.ReadValue<Vector2>();
            currentRot.x += delta.x * sensX *Time.deltaTime;
            currentRot.y += delta.y * sensY * Time.deltaTime;
            currentRot.x = Mathf.Repeat(currentRot.x, 360);
            currentRot.y = Mathf.Clamp(currentRot.y, -80, 80);
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
