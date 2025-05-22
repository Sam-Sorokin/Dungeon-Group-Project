using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttacks : WeaponBase
{
    [Header("Sounds")]
    
    [Header("Animators")]
    public Animator leftArm;
    public Animator rightArm;
    [Header("Prefabs")]
    public GameObject grenade;
    public GameObject fireBall;
    [Header("Attribute Values")]
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
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time >= altNextFireTime)
        {
            altNextFireTime = Time.time + altFireRate;
            AltFire();
        }
    }

    public override void MainFire()
    {
        rightArm.SetTrigger("FireShoot");
        ShootProjectile(fireBall, weaponOrigin, fireBallSpeed);
    }

    public override void AltFire()
    {
        leftArm.SetTrigger("GrenadeThrow");
        ThrowProjectile(grenade, weaponOrigin, GrenadeThrowForce);
        shotTheGun?.Invoke(); // Invoke UnityEvent for effects like gun recoil
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }
}
