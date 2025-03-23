using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Properties;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    protected int health = 100;
    // Start is called before the first frame update
    public virtual void TakeDamage(int _damageAmount) // Made virtual in the case of overriding to account for armour etc
    {
        health -= _damageAmount;
    }
    public virtual void GiveHealth(int _healingAmount)
    {
        health += _healingAmount;
    }
    public virtual void Death()
    {
        Object.Destroy(this.gameObject);
    }
    public int GetHealth()
    {
        return health;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Death();
        }
    }
}
