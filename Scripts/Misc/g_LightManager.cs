using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_LightManager : MonoBehaviour {
    [SerializeField]
    List<GameObject> PointLights = new List<GameObject>();
    [SerializeField]
    g_LightIntensity AmbientLight;

    public void SetLightSpeed(float speed)
    {
        for (int i = 0; i < PointLights.Count; i++)
        {
            PointLights[i].GetComponent<g_LightBounce>().speed = speed;
        }
    }
    public void ChangeToHitColour()
    {
        for (int i = 0; i < PointLights.Count; i++)
        {
            PointLights[i].GetComponent<g_LightColour>().ChangeToHitColour();
            AmbientLight.ChangeToHighIntensity();
        }
    }

    public void ChangeToKillColour()
    {
        for (int i = 0; i < PointLights.Count; i++)
        {
            PointLights[i].GetComponent<g_LightColour>().ChangeToKillColour();
            AmbientLight.ChangeToHighIntensity();
        }
    }
}
