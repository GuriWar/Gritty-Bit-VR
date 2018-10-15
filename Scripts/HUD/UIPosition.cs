using UnityEngine;
using System.Collections;

public class UIPosition : MonoBehaviour 
{
	[SerializeField]
	Transform m_UITransform;
    [SerializeField]
    Vector3 positionOffset;
    Vector3 targetPosition;
	[SerializeField]
	Vector3 m_angle;
    [SerializeField]
    float speed;
    [SerializeField]
    bool snapToTarget;
	Vector3 lastFramePosition;
    Vector3 lastFrameRotation;
    Quaternion targetRotation;
    enum RotateTypes
    {
        Normal,
        KeepY,
        AddAngles,
    }
    [SerializeField]
    RotateTypes rotateType;
    [SerializeField]
    bool rotateLocal;
	// Update is called once per frame
	void Update () 
	{
        targetPosition = m_UITransform.position + positionOffset;
        transform.position = targetPosition;
        lastFramePosition = transform.position;

        if (!rotateLocal)
        {
            if (rotateType == RotateTypes.KeepY)
                RotateKeepY();
            else if (rotateType == RotateTypes.Normal)
                Rotate();
            else
                RotateAddAngles();
        }
        else
        {
            if (rotateType == RotateTypes.KeepY)
                RotateKeepYLocal();
            else if (rotateType == RotateTypes.Normal)
                RotateLocal();
            else
                RotateAddAnglesLocal();
        }
    }
    void Rotate()
    {
        targetRotation = Quaternion.Euler(m_angle);
        if (snapToTarget)
            transform.eulerAngles = new Vector3(m_angle.x, m_angle.y, m_angle.z);
        else
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed*Time.deltaTime);
    }
    void RotateKeepY()
    {
        targetRotation = Quaternion.Euler(new Vector3(m_angle.x, m_UITransform.eulerAngles.y, m_angle.z));

        if (snapToTarget)
        {
            transform.eulerAngles = new Vector3(m_angle.x, m_UITransform.eulerAngles.y, m_angle.z);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    void RotateAddAngles()
    {
        targetRotation = Quaternion.Euler(new Vector3(m_UITransform.eulerAngles.x + m_angle.x, m_UITransform.eulerAngles.y + m_angle.y, m_UITransform.eulerAngles.z + m_angle.z));
        if (snapToTarget)
            transform.eulerAngles = new Vector3(m_UITransform.eulerAngles.x + m_angle.x, m_UITransform.eulerAngles.y + m_angle.y, m_UITransform.eulerAngles.z + m_angle.z);
        else
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    void RotateLocal()
    {
        targetRotation = Quaternion.Euler(m_angle);
        if (snapToTarget)
            transform.localEulerAngles = new Vector3(m_angle.x, m_angle.y, m_angle.z);
        else
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, speed * Time.deltaTime);
    }
    void RotateKeepYLocal()
    {
        targetRotation = Quaternion.Euler(new Vector3(m_angle.x, m_UITransform.eulerAngles.y, m_angle.z));
        if (snapToTarget)
            transform.localEulerAngles = new Vector3(m_angle.x, m_UITransform.eulerAngles.y, m_angle.z);
        else
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, speed * Time.deltaTime);
    }

    void RotateAddAnglesLocal()
    {
        targetRotation = Quaternion.Euler(new Vector3(m_UITransform.localEulerAngles.x + m_angle.x, m_UITransform.localEulerAngles.y + m_angle.y, m_UITransform.localEulerAngles.z + m_angle.z));
        if (snapToTarget)
            transform.localEulerAngles = new Vector3(m_UITransform.localEulerAngles.x + m_angle.x, m_UITransform.localEulerAngles.y + m_angle.y, m_UITransform.localEulerAngles.z + m_angle.z);
        else
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, speed * Time.deltaTime);
    }
}
