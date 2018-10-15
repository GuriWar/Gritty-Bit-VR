using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VRButtons : MonoBehaviour {
    [SerializeField]
    Color defaultColor;
    [SerializeField]
    Color selectedColor;
    bool selected;
    Button button;
    public enum ChangeTypes { text, image};
    [SerializeField]
    bool showSomething;
    [SerializeField]
    GameObject showObject;
    [SerializeField]
    ChangeTypes changeType;
    void Start()
    {
        button = GetComponent<Button>();
    }
    void Update()
    {
        if (selected)
        {
            if (changeType == ChangeTypes.text)
                GetComponentInChildren<Text>().color = selectedColor;
            else
                button.image.color = selectedColor;
            if (showSomething)
            {
                showObject.SetActive(true);
            }
        }
        else
        {
            if (changeType == ChangeTypes.text)
                GetComponentInChildren<Text>().color = defaultColor;
            else
                button.image.color = defaultColor;

            if (showSomething)
            {
                showObject.SetActive(false);
            }
        }
        selected = false;
    }
    public void Select()
    {
        selected = true;
    }
}
