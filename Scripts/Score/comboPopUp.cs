using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class comboPopUp : MonoBehaviour {

    Text combo;
    [SerializeField]
    float moveUpSpeed;
    [SerializeField]
    float fadeOutSpeed;
    int comboCount;
    // Use this for initialization
    void Start()
    {

        comboCount = GameObject.Find("Player").GetComponent<scoreTracker>().comboCount-1;
        combo = GetComponent<Text>();
        if (comboCount > 1)
        {
            combo.enabled = true;
            combo.text = "x" + comboCount.ToString();
        }
        else
        {
            combo.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

        //transform.parent = null;
        //transform.LookAt(GameObject.Find("PlayerCamera").transform);
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 1, 0), Time.deltaTime * moveUpSpeed);
        Color temp = combo.color;
        temp.a -= fadeOutSpeed * Time.deltaTime;
        combo.color = temp;
        // Debug.Break();
    }
}
