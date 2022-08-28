//Controlling the Airplane

using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class airplaneControl : MonoBehaviour
{
	//Input
	//Movement
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public WheelCollider frontWheel;
	private bool parkBrakeOn;
	//Axis
	public JoystickRotation joystick;
	private float Vertical;
	private float Horizontal;
	private float Yaw;

	//Control Surface
	public Surfaces elevatorLeft;
    public Surfaces elevatorRight;
	public Surfaces aileronLeft;
	public Surfaces aileronRight;
	public Surfaces rudder;
	public Rigidbody Rigidbody;

    private void Awake()
	{
		Rigidbody = GetComponent<Rigidbody>();
		parkBrakeOn = false;

		//Wake the wheels
		frontWheel.motorTorque = 1;
	}

    // Update is called once per frame
    void Update()
	{
		DeflectionValues();
		Brake();
	}
	void DeflectionValues()
    {
		Horizontal = (joystick.transform.localEulerAngles.x > 180) ? joystick.transform.localEulerAngles.x - 360 : joystick.transform.localEulerAngles.x;
		Vertical = (joystick.transform.localEulerAngles.y > 180) ? joystick.transform.localEulerAngles.y - 360 : joystick.transform.localEulerAngles.y;
		Yaw = (joystick.transform.localEulerAngles.z > 180) ? joystick.transform.localEulerAngles.z - 360 : joystick.transform.localEulerAngles.z;

		elevatorLeft.target = -Vertical / joystick.verticalClamp;
		elevatorRight.target = -Vertical / joystick.verticalClamp;
		aileronLeft.target = Horizontal / joystick.horizontalClamp;
		aileronRight.target = -Horizontal / joystick.horizontalClamp;
		rudder.target = Yaw / joystick.yawClamp;
	}

	public void Brake()
    {
		if (Input.GetKey(KeyCode.Space)){parkBrakeOn = true; }
        else if (Input.GetKeyUp(KeyCode.Space)){parkBrakeOn = false; }

		if (Input.GetKeyDown(KeyCode.P))
		{
			if (!parkBrakeOn)	parkBrakeOn = true;
			else if (parkBrakeOn)	parkBrakeOn = false;
		}
		if (parkBrakeOn)
		{
			rightWheel.brakeTorque = 1000f;
			leftWheel.brakeTorque = 1000f;
		}
		else if (!parkBrakeOn && !Input.GetKey(KeyCode.Space))	//if parkbrake not on, engine on, and not pressing space, apply no brake
        {
			rightWheel.brakeTorque = 0f;
			leftWheel.brakeTorque = 0f;
		}
	}

	private void OnGUI()
    {
		const float msToKnots = 1.94384f;
		GUI.Label(new Rect(10, 40, 300, 20), string.Format("Speed: {0:0.0} knots", Rigidbody.velocity.magnitude * msToKnots));
		GUI.Label(new Rect(10, 80, 300, 20), string.Format("FPS: {0:0.0}", 1 / Time.deltaTime));
	}
}
