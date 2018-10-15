using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeadShotDisplay : MonoBehaviour {
    Text myText;
    [SerializeField]
    int headShots;
    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        headShots = GameObject.Find("Player").GetComponent<scoreTracker>().headShots;
        myText.text = headShots.ToString();
    }
}
