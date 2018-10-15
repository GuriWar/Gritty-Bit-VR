using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopOnlineScoreDisplay : MonoBehaviour {

    [SerializeField]
    int index;
    Text myText;
    int score;
    string name;
    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
        myText.text = " ";

    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Level Scripts").GetComponent<dreamloLeaderBoard>().ToStringArray() != null)
        {
            if (GameObject.Find("Level Scripts").GetComponent<dreamloLeaderBoard>().ToStringArray().Length > index)
            {
                score = GameObject.Find("Level Scripts").GetComponent<dreamloLeaderBoard>().ToScoreArray()[index].score;
                name = GameObject.Find("Level Scripts").GetComponent<dreamloLeaderBoard>().ToScoreArray()[index].playerName;
                myText.text = name + " " + score.ToString();
            }
            else
            {
                myText.text = " ";
            }
        }
    }
}
