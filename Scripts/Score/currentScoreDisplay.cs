﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class currentScoreDisplay : MonoBehaviour {

	Text myText;
	// Use this for initialization
	void Start()
	{
		myText = GetComponent<Text>();
	}
	// Update is called once per frame
	void Update ()
	{
		if (PlayerPrefs.GetInt("PlayerScore") != null)
		myText.text = PlayerPrefs.GetInt("PlayerScore").ToString();
	}
}
