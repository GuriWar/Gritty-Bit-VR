using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_PumpActionScript : MonoBehaviour
{
    [SerializeField]
    float minZ;
    [SerializeField]
    float maxZ;
    [SerializeField]
    Transform startTransform;
    [SerializeField]
    Transform endTransform;

    enum ShotgunStates
    {
        Unloaded,
        Halfloaded,
        Reloaded
    }

    ShotgunStates currentState;
	// Update is called once per frame
	void Update ()
    {
        if (transform.localPosition.z > maxZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, maxZ);
        }

        if (transform.localPosition.z < minZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, minZ);
        }

	    if (currentState == ShotgunStates.Unloaded)
        {
            if (transform.position == endTransform.position)
            {
                currentState = ShotgunStates.Halfloaded;
            }
        }
        else if (currentState == ShotgunStates.Halfloaded)
        {
            if (transform.position == startTransform.position)
            {
                currentState = ShotgunStates.Reloaded;
            }
        }
	}

    void ShotgunUnloaded()
    {
        currentState = ShotgunStates.Unloaded;
    }

    public void MovePump(float offset)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + offset);
    }
}
