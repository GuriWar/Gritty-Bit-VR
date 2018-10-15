using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_ThrowObject : MonoBehaviour {
    [SerializeField]
    float m_throwForce;
    public void Throw(Vector3 velocity)
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(velocity.x, velocity.y, velocity.z) * m_throwForce);
        GetComponent<Rigidbody>().AddTorque(transform.right * 300f);
        if (GetComponent<ObjectState>() != null)
        GetComponent<ObjectState>().BeginDeleteCountdown();
    }
}
