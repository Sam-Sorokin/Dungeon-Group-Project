using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSurface : MonoBehaviour
{
    public int damage = 50000;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("KillSurface detected collision");
    //    Damagable damagable = collision.collider.GetComponent<Damagable>();

    //    if (damagable != null)
    //    {
    //        Debug.Log("KillSurface damaged entity");
    //        damagable.TakeDamage(50000);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("KillSurface detected collision");
        Damagable damagable = other.GetComponent<Damagable>();

        if (damagable != null)
        {
            Debug.Log("KillSurface damaged entity");
            damagable.TakeDamage(50000);
        }
    }

    //private void OnTriggerEnter()
    //{
        
    //}
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    Debug.Log("KillSurface detected collision");
    //    Damagable damagable = hit.collider.GetComponent<Damagable>();

    //    if (damagable != null)
    //    {
    //        Debug.Log("KillSurface damaged entity");
    //        damagable.TakeDamage(50000);
    //    }
    //}
}
