//Animates the headsup display
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
	public Text temp;
	float avg;

	// Update is called once per frame
	void Update()
	{
		avg += ((Time.deltaTime / Time.timeScale) - avg) * 0.03f;
		temp.text = (1f / avg).ToString();
	
	}

	
}
