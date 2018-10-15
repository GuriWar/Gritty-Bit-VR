using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class g_UpgradeScreenManager : MonoBehaviour {
    [SerializeField]
    List<GameObject> Screens = new List<GameObject>();
    // Use this for initialization
    void OnEnable ()
    {
        OpenScreen(0);	
	}


	public void OpenScreen(int x)
    {
        for (int i = 0; i <Screens.Count; i++)
        {
            if (i != x)
            {
                Screens[i].SetActive(false);
            }
            else
            {
                GetComponent<g_UIToggleActive>().active = true;
                Screens[i].SetActive(true);
            }
        }
    }
}
