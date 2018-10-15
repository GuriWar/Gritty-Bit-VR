using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class topScoreDisplay : MonoBehaviour {
	[SerializeField]
	int index;
	Text myText;
	int score;
    string name;
	// Use this for initialization
	void Start()
	{
		myText = GetComponent<Text>();
	}
	// Update is called once per frame
	void Update ()
	{
        if (GameObject.Find("Level Scripts").GetComponent<SaveScoreToFile>().Scores.Count > index)
        {
            score = GameObject.Find("Level Scripts").GetComponent<SaveScoreToFile>().Scores[index].GetScoreValue();
            name = GameObject.Find("Level Scripts").GetComponent<SaveScoreToFile>().Scores[index].playerName;
            myText.text = name + " " + score.ToString();
        }
        else
        {
            myText.text = " ";
        }
	}
}
