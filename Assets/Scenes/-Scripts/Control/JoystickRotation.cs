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
			if (pressed)
			{
				
			}
		}
		var direction = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z) - transform.position;
		var rotation = Quaternion.LookRotation(direction);

		rotation.eulerAngles = new Vector3(Mathf.Clamp(rotation.eulerAngles.x, 30, 330), rotation.eulerAngles.y, rotation.eulerAngles.z);
		transform.rotation = rotation;
	}

    public void Grab(InputAction.CallbackContext gripPressed)
    {
		pressed = gripPressed.ReadValueAsButton();
    }
}



