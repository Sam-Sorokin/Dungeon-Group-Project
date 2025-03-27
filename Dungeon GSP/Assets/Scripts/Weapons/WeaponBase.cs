using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBase : MonoBehaviour
{
    public UnityEvent shotTheGun;
    public string weaponName;
    protected int damage = 50;
    public float fireRate = 0.5f; // Fire rate in seconds
    private float nextFireTime = 0f; // Tracks when the weapon can fire again

    void Update()
    {
        handleInput();
    }

    public virtual void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextFireTime) // only allow fire if enough time as passed
        {
            nextFireTime = Time.time + fireRate; // Set next fire time
            MainFire();
        }
    }

    public void ShootBullet(Vector3 _startPos, Vector3 _endPos)
    {
 
        Ray ray = new Ray(_startPos, _endPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit Entity");
            Damagable damagable = hit.collider.GetComponent<Damagable>();
            if (damagable != null)
            {
                Debug.Log("Damaged Entity");
                damagable.TakeDamage(damage); // if the other collision is damagable take damage
            }
        }
    }

    public virtual void MainFire()
    {
        ShootBullet(transform.position, transform.forward);
        shotTheGun?.Invoke(); // Invoke UnityEvent for effects like gun recoil
    }

    public virtual void AltFire()
    {
        ShootBullet(transform.position, transform.forward);
        shotTheGun?.Invoke(); // Invoke UnityEvent for effects like gun recoil
    }
}
