using UnityEngine;
using System.Collections;

public class g_HologramToPhysical : MonoBehaviour
{
    [HideInInspector]
    public g_HologramWeaponHolder weaponReplacement;
    [SerializeField]
    GameObject RedHologramMesh;

    public void ReplaceWeapon()
    {
        //print(transform.name);
        weaponReplacement.SpawnWeapon(GetComponent<weaponIdentifier>().weaponID);
    }

    //public void TransitionToRedMesh()
    //{
    //    RedHologramMesh.SetActive(true);
    //    BlueHologramMesh.SetActive(false);
    //}
    //
    //public void TransitionToBlueMesh()
    //{
    //    BlueHologramMesh.SetActive(true);
    //    RedHologramMesh.SetActive(false);
    //}

}
