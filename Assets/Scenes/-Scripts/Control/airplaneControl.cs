//Controlling the Airplane

using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class airplaneControl : MonoBehaviour
{
	//Input
	public Engine engine;
	//Movement
	private float throttle;
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
		parkBrakeOn = true;

		//Wake the wheels
		frontWheel.motorTorque = 1;
	}

    // Update is called once per frame
    void Update()
	{
		DeflectionValues();
		Thrust();
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
	public void Thrust()
    {
		if (Input.GetKey(KeyCode.LeftShift))
		{
			throttle += 1f * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftControl))
		{
			throttle -= 1f * Time.deltaTime;
		}
		throttle = Mathf.Clamp01(throttle);
		engine.throttle = throttle;
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
		GUI.Label(new Rect(10, 80, 300, 20), string.Format("FPS: {0:0.0}", 1 / Time.deltaTime));
	}
}
