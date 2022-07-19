//Controls Engine

using UnityEngine;

public class Engine : MonoBehaviour
{
	[Range(0, 1)]
	public float throttle;		//Input Throttle
	private float thrust;        //Total Thrust

	public bool canTurnEngineOn;
	public bool engineOn;
	public Rigidbody rb;

    private void Start()
    {
		engineOn = false;
		canTurnEngineOn = true;
    }
    private void FixedUpdate()
	{
		if (engineOn == false)
        {
			thrust = 0;
            if (canTurnEngineOn)
            {
				if (Input.GetKeyDown(KeyCode.E))
                {
					engineOn = true;
                }
            }
        }
		else if (engineOn == true)
        {
			thrust = 20000f;
        }

		rb.AddRelativeForce(Vector3.forward * thrust * throttle);
	}

}
