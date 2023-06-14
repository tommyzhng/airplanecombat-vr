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
	private float xRightStick;
	private bool rPressed;
	private Collider enteredTrigger;

	private void Awake()
	{
		rot = new Vector3(-horizontalClamp, verticalClamp, yawClamp); //rotation only up to the clamp limits
	}
    private void FixedUpdate()
    {
		if (rPressed & enteredTrigger) { RotateJoystick(); }
		else if (!rPressed & enteredTrigger)
		{ 
			enteredTrigger = null; 
			transform.localEulerAngles = new Vector3(0, 0, 0); 
		}
	}
	//Joystick Control
	private void RotateJoystick()
    {
		Vector3 direction = enteredTrigger.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation(direction);
		float x = ((transform.localEulerAngles.x + 540) % 360) - 180;
		float y = ((transform.localEulerAngles.y + 540) % 360) - 180;
		float z = (xRightStick * 30);

		if (Mathf.Abs(x) > horizontalClamp)
		{
			x = Mathf.Sign(x) * horizontalClamp;
		}
		if (Math.Abs(y) > verticalClamp)
		{
			y = Mathf.Sign(y) * horizontalClamp;
		}
		transform.localEulerAngles = new Vector3(x, y, z);
	}
	//Oculus Control
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RHand"))
        {
			enteredTrigger = other;
		}
	}
	public void RGrab(InputAction.CallbackContext rightGripPressed)
	{
		rPressed = rightGripPressed.ReadValueAsButton();
	}
	public void RThumbstickX(InputAction.CallbackContext x)
    {
		xRightStick = x.ReadValue<Vector2>().x;
    }

	//Keyboard Control
	public void Axis(InputAction.CallbackContext keyDown)
	{
		transform.localEulerAngles = Vector3.Scale(rot, keyDown.ReadValue<Vector3>());
	}
	//Mouse Control
	public void MouseAxis(InputAction.CallbackContext mousePos)     //Set deflection based on mouse position - more accurate
	{
		if (!Input.GetKey(KeyCode.V))
		{
			Vector2 pos = mousePos.ReadValue<Vector2>();                //get vector 2 value from input
			pos.x -= Screen.width / 2;
			pos.y -= Screen.height / 2;

			if (!Input.GetMouseButton(0))
			{
				yaw = 0f;
				horizontal = 2 * (Mathf.InverseLerp(-Screen.width / 2, Screen.width / 2, pos.x) - 0.5f);        //Inverse Lerp gives us how far along the 
			}                                                                                                   //mouse is between the two values
			else
			{
				horizontal = 0f;
				yaw = 2 * (Mathf.InverseLerp(-Screen.width / 2, Screen.width / 2, pos.x) - 0.5f);
			}
			vertical = 2 * (Mathf.InverseLerp(-Screen.height / 2, Screen.height / 2, pos.y) - 0.5f);

			if (pos.x > -150 & pos.x < 150 & pos.y > -10 & pos.y < 10)  //Deadzone where horizontal deflection is zero
			{
				transform.localEulerAngles = Vector3.zero;
			}
			else
			{
				transform.localEulerAngles = Vector3.Scale(new Vector3(horizontal, vertical, yaw), rot);
			}
		}
    }
}



