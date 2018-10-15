using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController : MonoBehaviour {
    public enum Hands
    {
        LeftHand,
        RightHand
    };
    public Hands hand;
    [SerializeField]
    ControlType controller;
    Animation anim;
    float m_vibrateTime;
    float m_shootVibrateTime = .1f;
    float m_hitVibrateTime = .4f;
    float m_interactPromptTime = .1f;
    float m_currentVibrateTime;
    bool vibrate;

    void Start()
    {
        anim = GetComponent<Animation>();

    }
    // Update is called once per frame
    void Update ()
    {
        if (vibrate)
        {
            m_currentVibrateTime += Time.deltaTime;
            if (m_currentVibrateTime >= m_vibrateTime)
            {
                m_currentVibrateTime = 0;
                if (hand == Hands.LeftHand)
                {
                    StopVibration();
                }
                else
                {
                    StopVibration();
                }
                vibrate = false;
            }
        }
    }

    public Vector3 GetVelocity()
    {
        if (hand == Hands.LeftHand)
        {
            return SteamVR_Controller.Input(1).velocity;
        }
        else
        {
            return SteamVR_Controller.Input(2).velocity;
        }
    }

    public void Hit()
    {
        SteamVR_Controller.Input(1).TriggerHapticPulse(500);
        SteamVR_Controller.Input(2).TriggerHapticPulse(500);
        m_vibrateTime = m_hitVibrateTime;
        vibrate = true;
    }

    public void Shoot()
    {
        if (hand == Hands.LeftHand)
        {
            SteamVR_Controller.Input(1).TriggerHapticPulse(50000);
        }
        else
        {
            SteamVR_Controller.Input(2).TriggerHapticPulse(50000);
        }
        m_vibrateTime = m_shootVibrateTime;
        vibrate = true;
    }

    public void InteractPrompt()
    {
        if (hand == Hands.LeftHand)
        {
            SteamVR_Controller.Input(1).TriggerHapticPulse(500);
        }
        else
        {
            SteamVR_Controller.Input(2).TriggerHapticPulse(500);
        }
        m_vibrateTime = m_interactPromptTime;
        vibrate = true;
    }

    public void StopVibration()
    {
        if (hand == Hands.LeftHand)
        {
            SteamVR_Controller.Input(1).TriggerHapticPulse(0);
        }
        else
        {
            SteamVR_Controller.Input(2).TriggerHapticPulse(0);
        }

        vibrate = false;
        m_vibrateTime = 0;

    }
}
