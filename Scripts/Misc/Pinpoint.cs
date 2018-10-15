using UnityEngine;
using System.Collections;

public class Pinpoint : MonoBehaviour {

	void OnDrawGizmos()
	{
		Gizmos.DrawSphere(transform.position, .5f);
	}
}
