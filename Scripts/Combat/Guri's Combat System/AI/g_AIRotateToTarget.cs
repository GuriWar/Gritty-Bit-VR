using UnityEngine;
using System.Collections;

public class g_AIRotateToTarget : MonoBehaviour {



	Transform targetTransform;
	[SerializeField]
	Transform FirePosition;
	Quaternion targetRotBody;
    Quaternion targetRotSpine;
	Quaternion lastFrameBody;
    Quaternion lastFrameSpineBone;
    [SerializeField]
    float spineRotationSpeed;
    [SerializeField]
	Transform spineBone;
    public Vector3 maximumSpineRotationAngles = new Vector3(90, 90, 90);
    [SerializeField]
    bool clampBodyRotation;
    [SerializeField]
    float bodyRotationSpeed;
    public Vector3 maximumBodyRotationAngles;
	Vector3 tempSpineLocalEulerAngles;
    Vector3 tempBodyLocalEulerAngles;

	void LateUpdate()
    {

		if (targetTransform) 
		{
             //Rotate the spine bone so the gun (roughly) aims at the target
             transform.rotation = Quaternion.FromToRotation(FirePosition.forward, targetTransform.position - FirePosition.position) * transform.rotation;

            if (clampBodyRotation)
            {
                tempBodyLocalEulerAngles = transform.localEulerAngles;

                //Stop our agent from breaking their back by rotating too far
                tempBodyLocalEulerAngles = new Vector3(ClampEulerAngles(tempBodyLocalEulerAngles.x, maximumBodyRotationAngles.x),
                    ClampEulerAngles(tempBodyLocalEulerAngles.y, maximumBodyRotationAngles.y),
                    ClampEulerAngles(tempBodyLocalEulerAngles.z, maximumBodyRotationAngles.z));

                transform.localEulerAngles = tempBodyLocalEulerAngles;
            }

             targetRotBody = transform.rotation;
             //Smoothly rotate to the new position.  
             transform.rotation = Quaternion.Slerp(lastFrameBody, targetRotBody, Time.deltaTime * bodyRotationSpeed);
             lastFrameBody = transform.rotation;

             //Rotate the spine bone so the gun (roughly) aims at the target
             spineBone.rotation = Quaternion.FromToRotation(FirePosition.forward, targetTransform.position - FirePosition.position) * spineBone.rotation;
             tempSpineLocalEulerAngles = spineBone.localEulerAngles;

             //Stop our agent from breaking their back by rotating too far
             tempSpineLocalEulerAngles = new Vector3(ClampEulerAngles(tempSpineLocalEulerAngles.x, maximumSpineRotationAngles.x),
                 ClampEulerAngles(tempSpineLocalEulerAngles.y, maximumSpineRotationAngles.y),
                 ClampEulerAngles(tempSpineLocalEulerAngles.z, maximumSpineRotationAngles.z));

             spineBone.localEulerAngles = tempSpineLocalEulerAngles;

             targetRotSpine = spineBone.rotation;
             //Smoothly rotate to the new position.  
             spineBone.rotation = Quaternion.Slerp(lastFrameSpineBone, targetRotSpine, Time.deltaTime * spineRotationSpeed);
             lastFrameSpineBone = spineBone.rotation;
		}
	}

	public void SetTargetTransform(Transform x)
	{ 
		targetTransform = x; 
	}

	float ClampEulerAngles(float r, float lim)
	{
		if (r > 180)
			r -= 360;

		r = Mathf.Clamp(r, -lim, lim);

		return r;
	}
}
