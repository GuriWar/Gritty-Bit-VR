using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionScript : MonoBehaviour {

    bool slowMotion = false;
    public float slowMotionPower = 100;
    [SerializeField]
    float decreaseRate;
    [SerializeField]
    GameObject UI;
    [SerializeField]
    PauseScript pauseScript;
	// Update is called once per frame
	void Update ()
    {
        if (!pauseScript.paused)
        {
            if (slowMotion)
            {
                slowMotionPower -= decreaseRate * Time.deltaTime;
                Time.timeScale = 0.5f;
                UI.SetActive(true);
            }
            else
            {
                UI.SetActive(false);
                Time.timeScale = 1f;
            }
        }
	}

    public void IncreaseSlowMotionPower()
    {
        if (!slowMotion)
        slowMotionPower += 10;
    }

    public void ActivateSlowMotion()
    {
        slowMotion = true;
    }

    public void DeactivateSlowMotion()
    {
        slowMotion = false;
    }
}
