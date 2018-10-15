using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeDisplay : MonoBehaviour {

    Text myText;
    [SerializeField]
    int time;
    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        time = (int)GameObject.Find("Level Scripts").GetComponent<gameState>().gameTime;
        myText.text = time.ToString();
    }
}
