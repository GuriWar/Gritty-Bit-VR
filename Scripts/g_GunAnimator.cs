using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_GunAnimator : MonoBehaviour {

    Animator animator;
    int pistolHash;
    int machineHash;
    int shotgunHash;
    void SetHashes()
    {
        pistolHash = Animator.StringToHash("Pistol");
        machineHash = Animator.StringToHash("MachineGun");
        shotgunHash = Animator.StringToHash("Shotgun");
    }
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        SetHashes();
    }

    void Update()
    {
        //constantly get the current weapon
        if (GetComponentInParent<ObjectState>() != null)
        {
            if (GetComponentInParent<ObjectState>().currentState == ObjectState.ObjectStates.Held)
            {
                int gunMode = GetComponentInParent<handScript>().currentWeapon.ID;
                if (gunMode == 0)
                {
                    PistolTransition();
                }
                else if (gunMode == 1)
                {
                    MachineTransition();
                }
                else if (gunMode == 2)
                {
                    ShotgunTransition();
                }
            }
        }
    }

    public void PistolTransition()
    {
        animator.SetBool(shotgunHash, false);
        animator.SetBool(machineHash, false);
        animator.SetBool(pistolHash, true);
    }

    public void MachineTransition()
    {
        animator.SetBool(shotgunHash, false);
        animator.SetBool(machineHash, true);
        animator.SetBool(pistolHash, false);
    }

    public void ShotgunTransition()
    {
        animator.SetBool(shotgunHash, true);
        animator.SetBool(machineHash, false);
        animator.SetBool(pistolHash, false);
    }

}
