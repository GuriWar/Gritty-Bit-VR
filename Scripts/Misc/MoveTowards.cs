using UnityEngine;
using System.Collections;

public class MoveTowards : MonoBehaviour {

    [SerializeField]
    Transform m_target;
    [SerializeField]
    Transform m_lookTarget;
    [SerializeField]
    float m_speed;
	
    void Start()
    {
        if (m_lookTarget == null)
            m_lookTarget = m_target;
    }
	// Update is called once per frame
	void Update () {
        transform.LookAt(m_lookTarget);
        transform.position = Vector3.MoveTowards(transform.position, m_target.position, m_speed*Time.deltaTime);

	}
}
