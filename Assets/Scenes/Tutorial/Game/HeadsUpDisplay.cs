//Animates the headsup display
using System;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
	public Text temp;
	public Text speed;
	public Text MSL;
	private Rigidbody rbPlane;
	float avg;

    private void Awake()
    {
        rbPlane = GetComponentInParent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
	{
		avg += ((Time.deltaTime / Time.timeScale) - avg) * 0.03f;
		temp.text = Mathf.Round(1f / avg).ToString();

		speed.text = Math.Round(rbPlane.velocity.magnitude * 1.94384f, 1).ToString();
		MSL.text = Math.Round(rbPlane.transform.position.y, 1).ToString();
		
	}

	
}
