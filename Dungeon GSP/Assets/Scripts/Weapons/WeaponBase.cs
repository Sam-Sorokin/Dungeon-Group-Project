using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBase : MonoBehaviour
{
    public Transform weaponOrigin;
    public UnityEvent shotTheGun;
    public string weaponName;
    public int damage = 50;
    public float attackRange = 0.2f;
    public float fireRate = 0.5f; // Fire rate in seconds
    protected float nextFireTime = 0f; // Tracks when the weapon can fire again
    protected Vector3 attackPoint = new Vector3(0, 0, 1f);
    public GameObject hitEffectPrefab; // Assign this in the Inspector


    private void Start()
    {
    }

    void Update()
    {
        handleInput();
    }

    protected void OnDrawGizmosSelected() // drawing the attack ranges to visualise the distance in the scene view
    {
        Gizmos.color = Color.red;
        Vector3 GizmoPos = weaponOrigin.position;
        Gizmos.DrawLine(weaponOrigin.position, weaponOrigin.position + attackPoint*attackRange);
    }

    public virtual void handleInput()
    {
        if (Input.GetKey(KeyCode.Mouse0)) // only allow fire if enough time as passed
        {
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
                if (hitEffectPrefab != null)
                {
                    GameObject hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(hitEffect, 2f);
                }
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

    public void ShootProjectile(GameObject _projectile, Transform _throwOrigin, float _speed)
    {
        float spawnOffset = 1f;
        GameObject projectile = Instantiate(_projectile, _throwOrigin.transform.position + _throwOrigin.transform.forward * spawnOffset, _throwOrigin.rotation);
        Rigidbody projectileRB = projectile.GetComponent <Rigidbody>();

        if(projectileRB != null)
        {
            projectileRB.useGravity = false; // Don't want it affected by gravity as it shoots straight.
            projectileRB.velocity = _throwOrigin.forward * _speed;
        }
    }

    public virtual void MainFire()
    {
        if(Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // Set next fire time
            Debug.Log("Fired A Shot");
            RayDamage(weaponOrigin.position, weaponOrigin.forward);
            shotTheGun?.Invoke(); // Invoke UnityEvent for effects like gun recoil
        }
    }

    public virtual void AltFire()
    {
        RayDamage(weaponOrigin.position, weaponOrigin.forward);
        shotTheGun?.Invoke(); // Invoke UnityEvent for effects like gun recoil
    }
}
