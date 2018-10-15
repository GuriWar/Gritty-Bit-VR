using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class g_UpgradeSlider : MonoBehaviour {
    public Slider slider;
	// Use this for initialization
	void Start ()
    {
        slider = GetComponent<Slider>();
	}
	
    public void AddToSlider()
    {
        slider.value++;
    }
}
