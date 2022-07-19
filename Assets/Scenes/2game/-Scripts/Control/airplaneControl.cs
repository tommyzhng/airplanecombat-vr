//Controlling the Airplane

using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class airplaneControl : MonoBehaviour
{
	//Movement
	public Engine engine;
	private float throttle;
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	private bool parkBrakeOn;

	//Axis
	private float Vertical;
	private float Horizontal;
	private float Yaw;

	//Control Surface
	public Surfaces elevator;
	public Surfaces aileronLeft;
	public Surfaces aileronRight;
	public Surfaces rudder;
	public Rigidbody Rigidbody;

    private void Awake()
	{
		Rigidbody = GetComponent<Rigidbody>();
		parkBrakeOn = true;
	}

    // Update is called once per frame
    void Update()
	{
		DeflectionValues();
		Brake();

	}

	void DeflectionValues()
    {
		if (elevator != null)
		{
			elevator.target = -Vertical;
		}
		if (aileronLeft != null)
		{
			aileronLeft.target = -Horizontal;
		}
		if (aileronRight != null)
		{
			aileronRight.target = Horizontal;
		}
		if (rudder != null)
		{
			rudder.target = Yaw;
		}

		if (engine != null)
		{
			throttle += Input.GetAxis("Fire1") * Time.deltaTime;
			throttle -= Input.GetAxis("Fire2") * Time.deltaTime;
			throttle = Mathf.Clamp01(throttle);
			engine.throttle = throttle;
		}
	}
	//Input
	public void Axis(InputAction.CallbackContext keyDown)
	{
		Horizontal = keyDown.ReadValue<Vector3>().x;		//Get values from input actions
		Vertical = keyDown.ReadValue<Vector3>().y;
		Yaw = keyDown.ReadValue<Vector3>().z;
	}
	public void MouseAxis(InputAction.CallbackContext mousePos)		//Set deflection based on mouse position - more accurate
    {
		Vector2 pos = mousePos.ReadValue<Vector2>();				//get vector 2 value from input
		pos.x -= Screen.width / 2;
		pos.y -= Screen.height /2;

		if (pos.x > -150 & pos.x < 150 & pos.y > -10 & pos.y < 10)	//Deadzone where horizontal deflection is zero
		{
			Horizontal = 0f;										
		}
        else
        {
			Horizontal = 2 * (Mathf.InverseLerp(-Screen.width/2, Screen.width/2, pos.x) - 0.5f);
			Vertical = 2 * (Mathf.InverseLerp(-Screen.height/2, Screen.height/2, pos.y) - 0.5f);
		}
		
		
	}
	public void Brake()
    {

		if (Input.GetKey(KeyCode.Space))
        {
			rightWheel.brakeTorque = 1000f;
			leftWheel.brakeTorque = 1000f;
		}
        else
        {
			rightWheel.brakeTorque = 0f;
			leftWheel.brakeTorque = 0f;
		}

		if (Input.GetKeyDown(KeyCode.P) && ((Rigidbody.velocity.magnitude * 1.94384f) <= 10))
		{
			if (!parkBrakeOn)
            {
				parkBrakeOn = true;
			}
			else if (parkBrakeOn)
            {
				parkBrakeOn = false;
            }
		}
		if (parkBrakeOn)
		{
			rightWheel.brakeTorque = 1000f;
			leftWheel.brakeTorque = 1000f;
		}
		else if (!parkBrakeOn && engine.engineOn == true && !Input.GetKey(KeyCode.Space))	//if parkbrake not on, engine on, and not pressing space, apply no brake
        {
			rightWheel.brakeTorque = 0f;
			leftWheel.brakeTorque = 0f;
			engine.canTurnEngineOn = false;
		}
	}

	private void OnGUI()
    {
		const float msToKnots = 1.94384f;
		GUI.Label(new Rect(10, 40, 300, 20), string.Format("Speed: {0:0.0} knots", Rigidbody.velocity.magnitude * msToKnots));
		GUI.Label(new Rect(10, 60, 300, 20), string.Format("Throttle: {0:0.0}%", throttle * 100.0f));
	}
}
