using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Damagable
{
    public GameObject particleSystem;
    public float explosionRadius = 5f;
    public float detonationTime = 5f;
    float timeToDetonate = 0f;
    public int explosionDamage = 5;


    // Start is called before the first frame update
    void Start()
    {
        timeToDetonate = Time.time + detonationTime;
    }

    public override void Death()
    {
        explode();
    }

    void checkDetonateTime()
    {
        if(Time.time >= timeToDetonate)
        {
            explode();
        }
    }

    void explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            Damagable damagable = hitCollider.GetComponent<Damagable>();
            if (damagable != null)
            {
                Debug.Log("Damaged Entity");
                damagable.TakeDamage(explosionDamage); // if the other collision is damagable take damage
            }
        }
        GameObject particles = Instantiate(particleSystem, transform.position, transform.rotation);
        Destroy(particles, 1f);
        Object.Destroy(transform.root.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       checkForDeath();
       checkDetonateTime();
    }
}
