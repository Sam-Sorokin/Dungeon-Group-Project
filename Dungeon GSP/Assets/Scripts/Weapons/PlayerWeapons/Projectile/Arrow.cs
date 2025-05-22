using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public GameObject blood;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Damagable damagable = other.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(damage);
            if(!other.CompareTag("Player"))
            {
                GameObject particles = Instantiate(blood, transform.position, transform.rotation);
                Destroy(particles, 1f);
            }
        }

        if (!other.CompareTag("Player") && !other.CompareTag("Weapon"))
        {
            rb.isKinematic = true;
            transform.parent = other.transform;
        }
    }
}
