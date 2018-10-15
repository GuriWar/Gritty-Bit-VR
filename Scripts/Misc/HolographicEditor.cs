using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolographicEditor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeHologramColor(Color newColor)
    {
        //print(newColor);
        Material mat = GetComponent<Renderer>().material;
        mat.SetColor("_HoloColor", newColor);

    }
}
