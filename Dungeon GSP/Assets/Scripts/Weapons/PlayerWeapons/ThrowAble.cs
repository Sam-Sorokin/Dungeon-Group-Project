using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAble : WeaponBase
{
    public GameObject grenade;
    public GameObject fireBall;
    public float fireBallSpeed = 20f;
    public float GrenadeThrowForce = 20f;
    // Start is called before the first frame update
    public override void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextFireTime) // only allow fire if enough time as passed
        {
            nextFireTime = Time.time + fireRate; // Set next fire time
            MainFire();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            nextFireTime = Time.time + fireRate;
            AltFire();
        }
    }

    public override void MainFire()
    {
        ShootProjectile(fireBall, weaponOrigin, fireBallSpeed);
    }

    public override void AltFire()
    {
        ThrowProjectile(GrenadeThrowForce, grenade, weaponOrigin);
        shotTheGun?.Invoke(); // Invoke UnityEvent for effects like gun recoil
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }
}
