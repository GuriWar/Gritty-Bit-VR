using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthDisplay : MonoBehaviour {

	Text myText;
	[SerializeField]
	int health;
    GameObject player;
	// Use this for initialization
	void Start()
	{
		myText = GetComponent<Text>();
        player = GameObject.Find("Player");
	}
	// Update is called once per frame
	void Update ()
	{
        if (player.GetComponent<g_PlayerHealthScript>())
        {
            health = (int)player.GetComponent<g_PlayerHealthScript>().CurrentHealth;
        }
        else
            health = 0;
       
        myText.text = health.ToString();
	}
}
