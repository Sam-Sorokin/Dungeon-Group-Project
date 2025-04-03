using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAble : WeaponBase
{
    public GameObject grenade;
    public float throwForce = 20f;
    // Start is called before the first frame update
    public override void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time >= nextFireTime) // only allow fire if enough time as passed
        {
            nextFireTime = Time.time + fireRate; // Set next fire time
            AltFire();
        }
    }

    public override void AltFire()
    {
        ThrowProjectile(throwForce, grenade);
        shotTheGun?.Invoke(); // Invoke UnityEvent for effects like gun recoil
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }
}
