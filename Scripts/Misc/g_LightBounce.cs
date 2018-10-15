using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_LightBounce : MonoBehaviour {
    [SerializeField]
    Vector3 MinPosition;
    [SerializeField]
    Vector3 MaxPosition;
    public float speed;
    Vector3 targetPosition;
    // Use this for initialization
    void Start ()
    {
        targetPosition = MinPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, Time.deltaTime * speed);

        if ((transform.localPosition - MinPosition).magnitude < 1)
        {
            targetPosition = MaxPosition;
        }
        else if ((transform.localPosition - MaxPosition).magnitude < 1)
        {
            targetPosition = MinPosition;
        }
	}
}
