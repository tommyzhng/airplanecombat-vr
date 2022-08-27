using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

public class JoystickRotation : MonoBehaviour
{

	public int horizontalClamp;
	public int verticalClamp;
	public int yawClamp;
	public Vector3 rot;
	//Axis
	private float horizontal;
	private float vertical;
	private float yaw;
	//Oculus
	private bool pressed = false;

	private void Awake()
	{
		rot = new Vector3(-horizontalClamp, verticalClamp, yawClamp);
	}

	private void Update()
	{
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
			Vector3 direction = other.transform.position - transform.position;
			var rotation = Quaternion.LookRotation(direction).eulerAngles;
			rotation.z = transform.rotation.z;
			transform.localRotation = Quaternion.Euler(rotation);
			if (pressed)
			{
				
			}
			
		}
		
	}

    public void Grab(InputAction.CallbackContext gripPressed)
    {
		pressed = gripPressed.ReadValueAsButton();
    }
}



