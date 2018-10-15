using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct Weapon
{
    public string name;
    public int ID;
    public enum FiringTypes {Pistol, AutoPistol, Shotgun };
    public FiringTypes firingType;
    public GameObject ProjectilePrefab;
    public GameObject MuzzleFlashPrefab;
    public float ProjectileCount;
    public float ProjectileSpread;
    public float overHeat;
    public float overHeatFiringRate;
    public float baseFireRate;
    public float fireRate;
    public float fireRateAddition;
    public float baseDamage;
    public float damage;
    public float damageAddition;
    public Slider slider;
    public bool tapFiring;
    public AnimationClip firingAnimation;
    public void Update()
    {
        if (slider != null)
        {
            fireRate = baseFireRate + fireRateAddition * slider.value;
            damage = baseDamage + damageAddition * slider.value;
        }
    }
}
public class g_WeaponHolder : MonoBehaviour {

    public List<Weapon> Weapons = new List<Weapon>();

    void Update()
    {
        for (int i=0; i < Weapons.Count; i ++)
        {
            Weapons[i].Update();
        }
    }
}
