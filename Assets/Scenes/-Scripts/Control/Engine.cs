//Controls Engine

using UnityEngine;

public class Engine : MonoBehaviour
{
	[Range(0, 1)]
	public float throttle;		//Input Throttle
	public float thrust;        //Total Thrust

	public bool engineOn;
	public Rigidbody rb;

    private void Start()
    {
		engineOn = false;
    }
    private void FixedUpdate()
	{
		if (engineOn == false)
        {
			if (Input.GetKeyDown(KeyCode.E))
            {
				engineOn = true;
            }
        }
		else if (engineOn == true)
        {
			rb.AddRelativeForce(Vector3.forward * thrust * throttle);
		}
		Throttle();
	}

	//For Keyboard Control - temporary

	public void Throttle()
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
	}

}
