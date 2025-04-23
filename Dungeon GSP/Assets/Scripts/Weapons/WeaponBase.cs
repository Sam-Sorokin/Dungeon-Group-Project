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

    public void RayDamage(Vector3 _startPos, Vector3 _endPos)
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

    public void ThrowProjectile(float _forceAmount, GameObject _projectile, Transform _throwOrigin)
    {
        float spawnOffset = 1f; // an offset to which the projectile is spawned from the throw origin
        GameObject projectile = Instantiate(_projectile, _throwOrigin.transform.position + _throwOrigin.transform.forward * spawnOffset, _throwOrigin.rotation);
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.AddForce(_throwOrigin.transform.forward * _forceAmount);
    }
    public virtual void MainFire()
    {
        RayDamage(camera.position, camera.forward);
        shotTheGun?.Invoke(); // Invoke UnityEvent for effects like gun recoil
    }

    public virtual void AltFire()
    {
        RayDamage(camera.position, camera.forward);
        shotTheGun?.Invoke(); // Invoke UnityEvent for effects like gun recoil
    }
}
