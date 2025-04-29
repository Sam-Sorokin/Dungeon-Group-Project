using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWeapon : WeaponBase
{
    public enemytracking enemytracking;
    [Header("Attack Range")]
    // the range at which the enemy will attempt the attack
    public float attackMinRange = 0f;
    public float attackMaxRange = 10f;

    private void OnDrawGizmosSelected() // drawing the attack ranges to visualise the distance in the scene view
    {
        Gizmos.color = Color.red;
        Vector3 GizmoPos = weaponOrigin.position;
        Vector3 GizmoPlus = GizmoPos;
        GizmoPlus.z += 5;
        Gizmos.DrawWireSphere(GizmoPos, attackMinRange);
        Gizmos.DrawWireSphere(GizmoPos, attackMaxRange);
        base.OnDrawGizmosSelected();
        //Gizmos.DrawLine(weaponOrigin.position, GizmoPlus);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void handleInput()
    {
        if(enemytracking.distanceFromPlayer <= attackMaxRange &&
            enemytracking.distanceFromPlayer >= attackMinRange)
        {
            if(Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                MainFire();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }
}
