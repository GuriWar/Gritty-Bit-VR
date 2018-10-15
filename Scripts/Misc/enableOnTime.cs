using UnityEngine;
using System.Collections;

public class enableOnTime : MonoBehaviour 
{
	bool currentlyEnabled = false;
	[SerializeField]
	float enableTime;
	float currentEnableTime;
	[SerializeField]
	GameObject objectToEnable;
	// Update is called once per frame
	void Update () 
	{
		if (currentlyEnabled) 
		{
			currentEnableTime += Time.deltaTime;
			if (currentEnableTime >= enableTime) 
			{
				Disable ();
				currentEnableTime = 0;
			}
		}
	}

	public void Enable()
	{
		currentlyEnabled = true;
		objectToEnable.SetActive (true);
	}

	public void Enable(float time)
	{
		currentlyEnabled = true;
		enableTime = time;
		objectToEnable.SetActive (true);
	}

	public void Disable()
	{
		currentlyEnabled = false;
		objectToEnable.SetActive (false);
	}


}
