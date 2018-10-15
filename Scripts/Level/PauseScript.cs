using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
    gameState.GameStates savedState;
    public bool paused;
    float timeScaleSaved;
    [SerializeField]
    PPEManager LeftEyePPE;
    [SerializeField]
    PPEManager RightEyePPE;
    [SerializeField]
    GameObject PauseMenu;
    [SerializeField]
    List<GameObject> DisableOnPause = new List<GameObject>();

    public void Pause()
    {
        paused = true;
        LeftEyePPE.ClearBlood();
        //RightEyePPE.ClearBlood();
        timeScaleSaved = Time.timeScale;
        savedState = GetComponent<gameState>().playerState;
        GetComponent<gameState>().playerState = gameState.GameStates.Paused;
        for (int i = 0; i < DisableOnPause.Count; i++)
        {
            DisableOnPause[i].SetActive(false);
        }
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        paused = false;
        Time.timeScale = timeScaleSaved;
        GetComponent<gameState>().playerState = savedState;
        for (int i = 0; i < DisableOnPause.Count; i++)
        {
            if (DisableOnPause[i].GetComponent<g_UIToggleActive>() != null)
            {
                if (DisableOnPause[i].GetComponent<g_UIToggleActive>().active)
                    DisableOnPause[i].SetActive(true);
            }
        }
        PauseMenu.SetActive(false);
    }
	
}
