//Controlling the Airplane

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System;

public class airplaneControl : MonoBehaviour
{
	//Wheels
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public WheelCollider frontWheel;
	public bool brakeApplied;
	//Axis
	public JoystickRotation joystick;
	private float Vertical;
	private float Horizontal;
	private float Yaw;
	//Control Surfaces
	public Surfaces elevatorLeft;
    public Surfaces elevatorRight;
	public Surfaces aileronLeft;
	public Surfaces aileronRight;
	public Surfaces rudder;
	public Rigidbody Rigidbody;

	//Airbrakes
	public Surfaces spoiler;

	public Vector3 centerOfMass;

	public IInputInteraction interaction { get; }

    private void Awake()
	{
		Rigidbody = GetComponent<Rigidbody>();
		Rigidbody.centerOfMass = centerOfMass;
		brakeApplied = true;
		//Wake the wheels
		frontWheel.motorTorque = 1;
		rightWheel.brakeTorque = 1000f; 
		leftWheel.brakeTorque = 1000f;
	}

    // Update is called once per frame
    void Update()
	{
		ControlSurfaceValues();
		Steering();
	}

	void ControlSurfaceValues()
    {
		//apply equivalent rotations if past 180deg
		Horizontal = (joystick.transform.localEulerAngles.x > 180) ? joystick.transform.localEulerAngles.x - 360 : joystick.transform.localEulerAngles.x;		
		Vertical = (joystick.transform.localEulerAngles.y > 180) ? joystick.transform.localEulerAngles.y - 360 : joystick.transform.localEulerAngles.y;
		Yaw = (joystick.transform.localEulerAngles.z > 180) ? -(joystick.transform.localEulerAngles.z - 360) : -(joystick.transform.localEulerAngles.z);

		//send target angle movement
		elevatorLeft.target = -Vertical / joystick.verticalClamp;
		elevatorRight.target = -Vertical / joystick.verticalClamp;
		aileronLeft.target = Horizontal / joystick.horizontalClamp;
		aileronRight.target = -Horizontal / joystick.horizontalClamp;
		rudder.target = Yaw / joystick.yawClamp;
		
	}

	
	public void Steering()
	{
		//Steering angle opposite of yaw deflection
		float velocity = (float) Math.Round(Rigidbody.velocity.magnitude, 1);
		if (velocity < 100)
		{
			float steer = -Yaw;
			float clamp = (30 / (velocity * 0.1f));

			//Clamp angle based on speed of plane
			if (Mathf.Abs(steer) > clamp)
			{
				steer = Mathf.Sign(-Yaw) * clamp;
			}
			frontWheel.steerAngle = steer;
		}	
	}

	//##Oculus Input##\\
	public void Brake(InputAction.CallbackContext brakeButton)
	{
		if (brakeButton.interaction is HoldInteraction)
		{
			if (brakeButton.canceled)		// Cancelled - brake has been held for < 0.2s, activate brakes till pressed again
			{
				brakeApplied = brakeApplied ? false : true;
			}
			else if (brakeButton.performed)			//Performed - brake has been held for > 0.2s, activate brakes when held
			{
				brakeApplied = true;
			}
		}
		if (brakeApplied) { rightWheel.brakeTorque = 1000f; leftWheel.brakeTorque = 1000f; } //"lbl box 'brake applied'"
		else { rightWheel.brakeTorque = 0f; leftWheel.brakeTorque = 0f; }
	}
	public void Airbrake(InputAction.CallbackContext airBrakeButton)
    {
		if (airBrakeButton.started) { spoiler.target = (spoiler.target == 0) ? -1 : 0;}
    }

	//Debug
	private void OnGUI()
    {
		GUI.Label(new Rect(10, 35, 300, 20), string.Format("FPS: {0:0.0}", 1 / Time.deltaTime));
	}
}
