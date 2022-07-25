//Animates the headsup display
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
	public airplaneControl plane;
	public Image fpm;
	public Camera FPV;

	

	// Update is called once per frame
	void Update()
	{

		Vector3 position = Vector3.zero;
		position = FPV.WorldToScreenPoint(plane.transform.position + (plane.Rigidbody.velocity.normalized * 500f));
		fpm.transform.position = position;
	}

	
}
