﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class killsDisplay : MonoBehaviour
{
    Text myText;
    [SerializeField]
    int kills;
    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        kills = GameObject.Find("Player").GetComponent<scoreTracker>().kills;
        myText.text = kills.ToString();
    }
}
