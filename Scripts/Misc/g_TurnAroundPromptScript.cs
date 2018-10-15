using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_TurnAroundPromptScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (PlayerPrefs.GetInt("Degrees") == 360)
            gameObject.SetActive(false);
	}

}
