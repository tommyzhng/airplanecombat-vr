//Controls Engine

using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
	[Range(0, 1)]
	public float throttle;		//Input Throttle
	public float thrust;        //Total Thrust
	public Rigidbody rb;

	//Rotation Interpretor Variables
	public ThrottleRotation throttleRotation;

	private void FixedUpdate()
	{
		RotationInterpretation();
		rb.AddRelativeForce(Vector3.forward * thrust * throttle);
	}
	public void RotationInterpretation()
    {
		throttle = Mathf.InverseLerp(170, 270, throttleRotation.transform.localEulerAngles.y);
	}
    private void OnGUI()
    {
		GUI.Label(new Rect(10, 60, 300, 20), string.Format("Throttle: {0:0.0}%", throttle * 100.0f));
	}

}
