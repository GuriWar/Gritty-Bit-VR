using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class ControlType : MonoBehaviour {
	public enum Controllers
	{
		XboxController,
		Touch
	}
	public Controllers controller;

    public enum VRDevices
    {
        OculusRift,
        Vive
    }
    [HideInInspector]
    public VRDevices device;

    void Start()
    {
        if (UnityEngine.XR.XRDevice.model == "Oculus Rift CV1")
        {
            device = VRDevices.OculusRift;
        }
        else if (UnityEngine.XR.XRDevice.model == "Vive MV")
        {
            device = VRDevices.Vive;
        }

    }
}
