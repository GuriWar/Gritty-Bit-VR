using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class g_RadialMenu : MonoBehaviour {
    [SerializeField]
    List<Button> Buttons = new List<Button>();
    Button m_currentButton;
 
    // Update is called once per frame
    void Update ()
    {
        if (GetComponentInParent<MotionController>().hand == MotionController.Hands.LeftHand)
        {
            if (Input.GetAxisRaw("LeftJoystick_Vertical") > 0.3f && Input.GetAxisRaw("LeftJoystick_Horizontal") < 0.6f && Input.GetAxisRaw("LeftJoystick_Horizontal") > -0.6f)
            {
                m_currentButton = Buttons[0];
                Buttons[1].interactable = true;
                Buttons[2].interactable = true;
            }
            else if (Input.GetAxisRaw("LeftJoystick_Vertical") > -1 && Input.GetAxisRaw("LeftJoystick_Vertical") < 0.3f && Input.GetAxisRaw("LeftJoystick_Horizontal") > 0)
            {
                Buttons[0].interactable = true;
                m_currentButton = Buttons[1];
                Buttons[2].interactable = true;
            }
            else if (Input.GetAxisRaw("LeftJoystick_Vertical") > -1 && Input.GetAxisRaw("LeftJoystick_Vertical") < 0.3f && Input.GetAxisRaw("LeftJoystick_Horizontal") < 0)
            {
                Buttons[0].interactable = true;
                Buttons[1].interactable = true;
                m_currentButton = Buttons[2];
            }
        }
        else
        {
            if (Input.GetAxisRaw("RightJoystick_Vertical") > 0.3f && Input.GetAxisRaw("RightJoystick_Horizontal") < 0.6f && Input.GetAxisRaw("RightJoystick_Horizontal") > -0.6f)
            {
                m_currentButton = Buttons[0];
                Buttons[1].interactable = true;
                Buttons[2].interactable = true;
            }
            else if (Input.GetAxisRaw("RightJoystick_Vertical") > -1 && Input.GetAxisRaw("RightJoystick_Vertical") < 0.3f && Input.GetAxisRaw("RightJoystick_Horizontal") > 0)
            {
                Buttons[0].interactable = true;
                m_currentButton = Buttons[1];
                Buttons[2].interactable = true;
            }
            else if (Input.GetAxisRaw("RightJoystick_Vertical") > -1 && Input.GetAxisRaw("RightJoystick_Vertical") < 0.3f && Input.GetAxisRaw("RightJoystick_Horizontal") < 0)
            {
                Buttons[0].interactable = true;
                Buttons[1].interactable = true;
                m_currentButton = Buttons[2];
            }
        }
        if (m_currentButton != null)
        {
            if (m_currentButton.interactable)
            {
                m_currentButton.onClick.Invoke();
                m_currentButton.interactable = false;
            }
        }
    }
}
