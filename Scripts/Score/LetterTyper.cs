using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LetterTyper : MonoBehaviour {

    public Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }
    public void AddToText(string letter)
    {
        if (text.text.Length < 5)
        text.text = text.text + letter;
    }

    public void Backspace()
    {
        char[] temp;
        temp = text.text.ToCharArray();
        text.text = "";
        for (int i = 0; i < temp.Length-1; i++)
        {
            text.text = text.text + temp[i];
        }
     
    }
}
