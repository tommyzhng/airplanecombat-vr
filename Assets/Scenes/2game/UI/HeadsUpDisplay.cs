//Animates the headsup display
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
	public airplaneControl plane;

	public Image fpm;
	public Image cross;
	public Camera FPV;

	const float kProjectionDistance = 500.0f;


	// Update is called once per frame
	void Update()
	{
		Vector3 position = Vector3.zero;

		if (cross != null)
		{
			// Put the cross some meters in front of the plane. This way the cross and FPM line up
			// correctly when there is zero angle of attack.
			position = FPV.WorldToScreenPoint(plane.transform.position + (plane.transform.forward.normalized * kProjectionDistance));
			//pos.z = 0.0f;
			cross.transform.position = position;
		}

		if (fpm != null)
		{
			// Put the cross some meters in front of the plane. This way the cross and FPM line up
			// correctly when there is zero angle of attack.
			position = FPV.WorldToScreenPoint(plane.transform.position + (plane.Rigidbody.velocity.normalized * kProjectionDistance));
			//pos.z = 0.0f;
			fpm.transform.position = position;
		}
		
	}
}
