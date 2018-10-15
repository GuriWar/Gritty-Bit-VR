using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPEManager : MonoBehaviour {

    [SerializeField]
    float damageScreenTime;
    float damageScreenCurrentTime;
    bool damageScreen;
	
	// Update is called once per frame
	void Update ()
    {

        //change the colour of the computer effect to red as damage is done
       // print(transform.root.GetComponent<vp_FPPlayerDamageHandler>().CurrentHealth / transform.root.GetComponent<vp_FPPlayerDamageHandler>().MaxHealth*2);
        if (damageScreen)
        {
            damageScreenCurrentTime += Time.deltaTime;
            GetComponent<CameraFilterPack_Vision_Blood_Fast>().enabled = true;
            GetComponent<CameraFilterPack_Vision_Blood_Fast>().HoleSize += Time.deltaTime;
            GetComponent<CameraFilterPack_Color_BrightContrastSaturation>().Saturation -= 5f*Time.deltaTime;
            if (GetComponent<CameraFilterPack_Color_BrightContrastSaturation>().Saturation < 1.5f)
                GetComponent<CameraFilterPack_Color_BrightContrastSaturation>().Saturation = 1.5f;
            if (damageScreenCurrentTime >= damageScreenTime)
            {
                damageScreen = false;
                GetComponent<CameraFilterPack_Vision_Blood_Fast>().enabled = false;

            }
        }

	}

    public void DoDamage()
    {
        GetComponent<CameraFilterPack_Vision_Blood_Fast>().HoleSize = 0.5f;
        GetComponent<CameraFilterPack_Color_BrightContrastSaturation>().Saturation = 2.5f;
        damageScreen = true;
        damageScreenCurrentTime = 0;
    }

    public void ClearBlood()
    {
        damageScreen = false;
        GetComponent<CameraFilterPack_Color_BrightContrastSaturation>().Saturation = 1.5f;

        GetComponent<CameraFilterPack_Vision_Blood_Fast>().enabled = false;
    }
}
