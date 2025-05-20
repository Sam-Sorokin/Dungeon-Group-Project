using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMelee : WeaponBase
{
    public Animator animator;
    public enemytracking enemytracking;
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
    }

    public override void handleInput()
    {
        if(enemytracking.iswalking == false &&
            enemytracking.distanceFromPlayer <= enemytracking.trackDistance)
        {
            if(Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                MainFire();
            }
        }
    }

    public override void MainFire()
    {
        //ThrowProjectile(arrow, weaponOrigin, arrowSpeed);
        animator.SetTrigger("Attack");
    }

    void attack()
    {
        RayDamage(weaponOrigin.position, weaponOrigin.forward);
        Debug.Log("goblin attempted an attack");
    }


    // Update is called once per frame
    void Update()
    {
        handleInput();
    }
}
