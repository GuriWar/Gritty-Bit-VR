using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_LightColour : MonoBehaviour {
    public Color defaultColour;
    Color currentColour;
    [SerializeField]
    Color hitColour;
    [SerializeField]
    Color killColour;
    [SerializeField]
    float speed;
	// Use this for initialization
	void Start ()
    {
        defaultColour = GetComponent<Light>().color;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (currentColour != defaultColour)
        {
            currentColour = Color.Lerp(currentColour, defaultColour, Time.deltaTime * speed);
        }
        GetComponent<Light>().color = currentColour;
	}

    public void ChangeToHitColour()
    {
        currentColour = hitColour;
    }

    public void ChangeToKillColour()
    {
        currentColour = killColour;
    }
}
