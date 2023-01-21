//Animates the headsup display
using System;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
	public Text FPS;
	public Text speed;
	public Text altMSL;
	public Text warnings;

	public GameObject altMeasure;
	public airplaneControl status;
	private Rigidbody rbPlane;
	float avg;

    private void Awake()
    {
        rbPlane = GetComponentInParent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
	{
		//FPS
		avg += ((Time.deltaTime / Time.timeScale) - avg) * 0.03f;
		FPS.text = (" FPS : " + Mathf.Round(1f / avg).ToString());

		//Speed
		speed.text = Math.Round(rbPlane.velocity.magnitude * 1.94384f, 1).ToString() + " kn";

		//ALtitude
		altMSL.text = Math.Round(altMeasure.transform.position.y * 3.28084 - 3.9, 0).ToString() + " ft";

		//Brake Warnings
		if (status.brakeApplied == true && status.spoiler.target == -1)
        {
			warnings.text = "BRAKES     SPOILERS";
		}
        else
        {
			if (status.brakeApplied == true)
				warnings.text = "BRAKES";
			else if (status.spoiler.target == -1)
				warnings.text = "SPOILERS";
			else
				warnings.text = "";
        }

	}
}
