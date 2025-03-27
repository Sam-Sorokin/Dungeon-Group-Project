using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBase : Damagable
{
    public int enemyHealth = 0;
    // Start is called before the first frame update
    void Start()
    {
        health = enemyHealth;
    }

    public virtual void Attack()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }
}
