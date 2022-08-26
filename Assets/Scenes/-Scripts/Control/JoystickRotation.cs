using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class JoystickRotation : MonoBehaviour
{
	public Vector2 horizontalClamp;
	public Vector2 verticalClamp;
	public Vector2 yawClamp;
	public Vector3 rot;
	//Axis
	private float horizontal;
	private float vertical;
	private float yaw;

    private void Awake()
    {
		rot = new Vector3(-horizontalClamp.x, verticalClamp.x, yawClamp.x);
	}


    // Update is called once per frame

    public void Axis(InputAction.CallbackContext keyDown)
	{
		transform.localEulerAngles = Vector3.Scale(rot, keyDown.ReadValue<Vector3>());        //Get values from input actions
	}

	public void MouseAxis(InputAction.CallbackContext mousePos)     //Set deflection based on mouse position - more accurate
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
