//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class TouchController : MonoBehaviour {
//    public enum Hands
//    {
//        LeftHand,
//        RightHand
//    };
//    public Hands hand;
//    [SerializeField]
//    ControlType controller;
//    Animation anim;
//    float m_vibrateTime;
//    float m_shootVibrateTime = .1f;
//    float m_hitVibrateTime = .4f;
//    float m_interactPromptTime = .1f;
//    float m_currentVibrateTime;
//    bool vibrate;
//    float zLastFrame;
//
//    void Start()
//    {
//        anim = GetComponent<Animation>();
//
//    }
//    // Update is called once per frame
//    void Update ()
//    {
//        if (vibrate)
//        {
//            m_currentVibrateTime += Time.deltaTime;
//            if (m_currentVibrateTime >= m_vibrateTime)
//            {
//                m_currentVibrateTime = 0;
//                if (hand == Hands.LeftHand)
//                {
//                    StopVibration();
//                }
//                else
//                {
//                    StopVibration();
//                }
//                vibrate = false;
//            }
//        }
//        if (controller.GetComponent<ControlType>().controller == ControlType.Controllers.Touch)
//        {
//                if (hand == Hands.LeftHand)
//                {
//                    transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
//                    transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
//                }
//                else
//                {
//                    transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
//                    transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
//                }
//            zLastFrame = transform.localPosition.z;
//        }
//    }
//
//    public Vector3 GetVelocity()
//    {
//        if (hand == Hands.LeftHand)
//        {
//            return OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
//        }
//        else
//        {
//            return OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
//        }
//    }
//
//    public void Hit()
//    {
//
//        OVRInput.SetControllerVibration(0, .3f, OVRInput.Controller.LTouch);
//        OVRInput.SetControllerVibration(0, .3f, OVRInput.Controller.RTouch);
//        m_vibrateTime = m_hitVibrateTime;
//        vibrate = true;
//    }
//
//    public void Shoot()
//    {
//        if (hand == Hands.LeftHand)
//        {
//            OVRInput.SetControllerVibration(0, .3f, OVRInput.Controller.LTouch);
//        }
//        else
//        {
//            OVRInput.SetControllerVibration(0, .3f, OVRInput.Controller.RTouch);
//        }
//        m_vibrateTime = m_shootVibrateTime;
//        vibrate = true;
//    }
//
//    public void InteractPrompt()
//    {
//        if (hand == Hands.LeftHand)
//        {
//            OVRInput.SetControllerVibration(0, .2f, OVRInput.Controller.LTouch);
//        }
//        else
//        {
//            OVRInput.SetControllerVibration(0, .2f, OVRInput.Controller.RTouch);
//        }
//        m_vibrateTime = m_interactPromptTime;
//        vibrate = true;
//    }
//
//    public void StopVibration()
//    {
//        if (hand == Hands.LeftHand)
//        {
//            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
//        }
//        else
//        {
//            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
//        }
//
//        vibrate = false;
//        m_vibrateTime = 0;
//
//    }
//
//}
