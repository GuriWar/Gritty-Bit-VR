
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class g_UILine : MonoBehaviour {
    public LayerMask layermask;
    Ray ray;
    LineRenderer line;
    gameState gameStateScript;
	// Use this for initialization
	void Start ()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(1, new Vector3(0, 0, 100));
        gameStateScript = GameObject.Find("Level Scripts").GetComponent<gameState>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameStateScript.playerState != gameState.GameStates.Wave && gameStateScript.playerState != gameState.GameStates.Pregame)
        {
            line.enabled = true;
            ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
            {
                if (hit.transform.gameObject.tag == "Button")
                {
                    if(hit.transform.GetComponent<VRButtons>()!= null)
                    hit.transform.GetComponent<VRButtons>().Select();
                }
            }
        }
        else
            line.enabled = false;
    }

    public void ButtonPressed()
    {
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
        {
            if (hit.transform.gameObject.tag == "Button")
            {
                GetComponent<AudioSource>().Play();
                hit.transform.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
