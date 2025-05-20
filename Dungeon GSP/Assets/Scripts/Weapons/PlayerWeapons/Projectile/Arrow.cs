using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
       //Damagable damagable = collision.gameObject.GetComponent<Damagable>();
       // if (damagable != null)
       // {
       //     damagable.TakeDamage(damage);
       // }
       // transform.parent = collision.transform;
    }
}
