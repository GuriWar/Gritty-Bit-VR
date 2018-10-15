using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour {

	Text myText;
	[SerializeField]
	int money;
    GameObject player;
    [SerializeField]
    bool indent;
	// Use this for initialization
	void Start()
	{
		myText = GetComponent<Text>();
        player = GameObject.Find("Player");
	}
	// Update is called once per frame
	void Update ()
	{
		money = player.GetComponent<PlayerMoneyScript>().money;
        if (indent)
		    myText.text = "Credits: \n$" + money.ToString();
        else
            myText.text = "Credits: $" + money.ToString();

    }
}
