using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_ObjectManager : MonoBehaviour {

    public List<GameObject> m_Objects = new List<GameObject>();
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    GameObject rightHand;
	
	// Update is called once per frame
	void Update ()
    {
        UpdateRaycastUI();
	}

    void UpdateRaycastUI()
    {
        GunUIScript tempUIScript;
        //check all the objects in level to see if we are aiming at them with our hands and adjust UI accordingly
        //this has separate variables for each hand because unlike the inrange function this will be called every frame
        //we dont want the 'selected' variable to be overwritten by a different hand's values
        for (int i =0; i < m_Objects.Count; i ++)
        {
              tempUIScript = m_Objects[i].GetComponentInChildren<GunUIScript>();
            //do the raycast from both hands, if both return false then target isn't in sight
              if (leftHand.GetComponent<handScript>().handState != handScript.HandStates.Wielding)
              {
                  if (leftHand.GetComponent<handScript>().RayToDistantObjects() != m_Objects[i])
                  {
                      tempUIScript.SetInRaycastSight(leftHand, false);
                  }
                  else
                  {
                      tempUIScript.SetInRaycastSight(leftHand, true);
                  }
              }
              if (rightHand.GetComponent<handScript>().handState != handScript.HandStates.Wielding)
              {
                  if (rightHand.GetComponent<handScript>().RayToDistantObjects() != m_Objects[i])
                  {
                      tempUIScript.SetInRaycastSight(rightHand, false);
                  }
                  else
                  {
                      tempUIScript.SetInRaycastSight(rightHand, true);
                  }
              }
         }
    }
}
