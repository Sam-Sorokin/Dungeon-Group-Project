using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBase : MonoBehaviour
{
    public Transform camera;
    public UnityEvent shotTheGun;
    public string weaponName;
    protected int damage = 50;
    public float attackRange = 0.2f;
    public float fireRate = 0.5f; // Fire rate in seconds
    protected float nextFireTime = 0f; // Tracks when the weapon can fire again


    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("Player Camera").transform;
    }

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
        Debug.DrawRay(_startPos, _endPos * attackRange, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackRange))
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

    public void ThrowProjectile(float _forceAmount, GameObject _projectile)
    {
        float camOffset = 5f;
        GameObject projectile = Instantiate(_projectile, camera.transform.position + camera.transform.forward * camOffset, camera.rotation);
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.AddForce(camera.transform.forward * _forceAmount);
    }
    public virtual void MainFire()
    {
        ShootBullet(camera.position, camera.forward);
        shotTheGun?.Invoke(); // Invoke UnityEvent for effects like gun recoil
    }

    public virtual void AltFire()
    {
        ShootBullet(camera.position, camera.forward);
        shotTheGun?.Invoke(); // Invoke UnityEvent for effects like gun recoil
    }
}
