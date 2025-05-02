using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSurface : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("KillSurface detected collision");
        Damagable damagable = collision.collider.GetComponent<Damagable>();

        if (damagable != null)
        {
            Debug.Log("KillSurface damaged entity");
            damagable.TakeDamage(50000);
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("KillSurface detected collision");
        Damagable damagable = hit.collider.GetComponent<Damagable>();

        if (damagable != null)
        {
            Debug.Log("KillSurface damaged entity");
            damagable.TakeDamage(50000);
        }
    }
}
