using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GunAmmo : MonoBehaviour {

    public float currentAmmo;
    [SerializeField]
    float clipAddition;
    [SerializeField]
    float baseAmmo;
    [HideInInspector]
    public Slider slider;
    public void CalculateInitialAmmo()
    {
        if (slider != null)
            currentAmmo = baseAmmo + clipAddition * slider.value;
        else
            currentAmmo = baseAmmo;
    }

}
