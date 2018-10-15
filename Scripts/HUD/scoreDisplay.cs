using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scoreDisplay : MonoBehaviour {

	Text myText;
	[SerializeField]
	int score;
	// Use this for initialization
	void Start()
	{
		myText = GetComponent<Text>();
	}
	// Update is called once per frame
	void Update ()
	{
		score = GameObject.Find("Player").GetComponent<scoreTracker>().score;
		myText.text = score.ToString();
	}
}
