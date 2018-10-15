/////////////////////////////////////////////////////////////////////////////////
//
//	vp_HitscanBullet.cs
//	© VisionPunk. All Rights Reserved.
//	https://twitter.com/VisionPunk
//	http://www.visionpunk.com
//
//	description:	a script for hitscan projectiles. this script should be
//					attached to a gameobject with a mesh to be used as the impact
//					decal (bullet hole)
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class g_SwordAttack : MonoBehaviour
{
    [SerializeField]
    ObjectState objectStateScript;
    MotionController speedScript;
    public float m_damage;
    private GameObject m_swordHit;
    public LayerMask layerMask;

    IEnumerator ApplyDamage()
    {
        m_swordHit.SendMessage("Damage", m_damage, SendMessageOptions.DontRequireReceiver);

        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (objectStateScript.currentState == ObjectState.ObjectStates.Held)
        {
            //raycast and transmit damage based on touch controller velocity
            if (GetComponentInParent<MotionController>().GetVelocity().magnitude > 1)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, other.transform.position, out hit, layerMask)) // ignoring layermask, did we hit something
                {
                    m_swordHit = other.gameObject;
                    StartCoroutine(ApplyDamage());
                }
            }
        }
        else if (objectStateScript.currentState == ObjectState.ObjectStates.Free)
        {
            //raycast and transmit damage based on rigidbody velocity
            if (GetComponentInParent<Rigidbody>().velocity.magnitude > 1)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, other.transform.position, out hit, layerMask)) // ignoring layermask, did we hit something
                {
                    m_swordHit = other.gameObject;
                    StartCoroutine(ApplyDamage());
                }
            }
        }
	}
}

