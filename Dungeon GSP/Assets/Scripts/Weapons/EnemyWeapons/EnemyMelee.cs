using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMelee : WeaponBase
{
    public Animator animator;
    public enemytracking enemytracking;
    // the range at which the enemy will attempt the attack
    public bool attacking = false;
    public float attackMinRange = 0f;
    public float attackMaxRange = 10f;

    private void OnDrawGizmosSelected() // drawing the attack ranges to visualize the distance in the scene view
    {
        Gizmos.color = Color.red;
        Vector3 GizmoPos = weaponOrigin.position;
        Vector3 forwardDirection = weaponOrigin.forward;  // Get the forward direction of the weapon

        // Optionally, offset the Gizmo from the weapon origin if you need it in front of the weapon
        Vector3 attackDirection = forwardDirection * attackMaxRange; // Example for max range

        // Drawing the range spheres to represent attack distance
        Gizmos.DrawWireSphere(GizmoPos, attackMinRange);
        Gizmos.DrawWireSphere(GizmoPos, attackMaxRange);

        // Draw lines to visualize attack direction and range
        Gizmos.DrawLine(GizmoPos, GizmoPos + attackDirection);  // Show the direction of the attack range
        base.OnDrawGizmosSelected();
    }

    public override void handleInput()
    {
        if(enemytracking.iSwalking == false &&
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
        enemytracking.attacking = true;
    }

    void attack()
    {
        RayDamage(weaponOrigin.position, weaponOrigin.forward);
        Debug.Log("goblin attempted an attack");
        enemytracking.attacking = false;
    }


    // Update is called once per frame
    void Update()
    {
        handleInput();
    }
}
