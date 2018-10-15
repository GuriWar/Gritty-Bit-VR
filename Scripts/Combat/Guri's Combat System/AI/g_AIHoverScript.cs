using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_AIHoverScript : MonoBehaviour {
    [SerializeField]
    Vector2 m_distanceRangeFromTarget;
    [SerializeField]
    Transform m_hoverTarget;
    [SerializeField]
    float m_hoverSpeed;
    [SerializeField]
    Vector3 movePosition;
    Vector3 lastFrame;
    g_AIRotateToTarget rotationScript;

    void Start()
    {
        m_hoverTarget = GameObject.Find("PlayerCamera").transform;
        movePosition = transform.position;
        rotationScript = GetComponent<g_AIRotateToTarget>();
    }
	// Update is called once per frame
	void Update ()
    {
	    //move to a position, once there get a new position and hover to there
        if ((transform.position - movePosition).magnitude < 1)
        {
            movePosition = m_hoverTarget.position + new Vector3(Random.Range(10, 15), Random.Range(0, 2), Random.Range(m_distanceRangeFromTarget.x, m_distanceRangeFromTarget.y));
            //has to be within a certain spot in the level
        }

        transform.position = Vector3.Slerp(lastFrame, movePosition, Time.deltaTime * m_hoverSpeed);

        lastFrame = transform.position;
    }

    public void SetHoverTarget(Transform target)
    {
        m_hoverTarget = target;
    }


}
