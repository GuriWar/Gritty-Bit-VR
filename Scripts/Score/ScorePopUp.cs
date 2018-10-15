using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScorePopUp : MonoBehaviour {
    Text score;
    [SerializeField]
    float moveUpSpeed;
    [SerializeField]
    float fadeOutSpeed;

	// Use this for initialization
	void Start ()
    {
        if (GetComponentInParent<UIPosition>() != null)
            GetComponentInParent<UIPosition>().enabled = false;
        score = GetComponent<Text>();
        score.text = transform.root.GetComponent<scoreWorth>().points.ToString();

	}
	
	// Update is called once per frame
	void Update ()
    {
        //transform.parent = null;
        //transform.LookAt(GameObject.Find("PlayerCamera").transform);
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 1, 0), Time.deltaTime * moveUpSpeed);
        Color temp = score.color;
        temp.a -= fadeOutSpeed * Time.deltaTime;
        score.color = temp;
       // Debug.Break();
	}
}
