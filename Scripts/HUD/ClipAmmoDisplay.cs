using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClipAmmoDisplay : MonoBehaviour
{
    
    Text myText;
    [SerializeField]
	GunAmmo m_ammo;
    [SerializeField]
    Vector3 leftHandPosition;
    [SerializeField]
    Vector3 rightHandPosition;
	// Use this for initialization
	void Start()
    {
        myText = GetComponentInChildren<Text>();
    }
	// Update is called once per frame
	void Update ()
    {
        if (GetComponentInParent<MotionController>() != null)
        {
            if (GetComponentInParent<MotionController>().hand == MotionController.Hands.LeftHand)
            {
                myText.transform.localPosition = leftHandPosition;
            }
            else
            {
                myText.transform.localPosition = rightHandPosition;
            }
            myText.text = m_ammo.currentAmmo.ToString();
        }
        else
        {
            myText.text = "";
        }

	}
}
