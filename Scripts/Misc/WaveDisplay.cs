using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveDisplay : MonoBehaviour {
    Text text;
    int waveNumber;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        text.text = "Wave " + waveNumber.ToString();
	}

    public void SetWaveNumber(int x)
    {
        waveNumber = x;
    }
}
