using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class g_HealthBar : MonoBehaviour {
    Image healthImage;
	// Use this for initialization
	void Start ()
    {
        healthImage = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //high health means high green
        // low health means high red
        if (transform.root.GetComponent<g_AIHealthScript>() != null)
        {
            healthImage.color = new Color(255 - (255 * (transform.root.GetComponent<g_AIHealthScript>().health / transform.root.GetComponent<g_AIHealthScript>().maxHealth)), 255 * (transform.root.GetComponent<g_AIHealthScript>().health / transform.root.GetComponent<g_AIHealthScript>().maxHealth), 0, 1);
            healthImage.fillAmount = transform.root.GetComponent<g_AIHealthScript>().health / transform.root.GetComponent<g_AIHealthScript>().maxHealth;
            //transform.eulerAngles = new Vector3(0, 0, 0);
            //print(healthImage.color);
        }
    }
}
