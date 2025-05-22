using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Properties;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 10;
    // Start is called before the first frame update
    public virtual void TakeDamage(int _damageAmount) // Made virtual in the case of overriding to account for armour etc
    {
        health -= _damageAmount;
        Debug.Log("TakenDamage");
    }
    public virtual void GiveHealth(int _healingAmount)
    {
        health += _healingAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public virtual void Death() // 
    {
        Debug.Log("Entity Died");
        Object.Destroy(this.gameObject);
    }
    public int GetHealth() // for other classes to get the health.
    {
        return health;
    }
    protected void checkForDeath() // calls death function when health drops to 0 or below.
    {
        if (health <= 0)
        {
            Death();
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkForDeath();
    }
}
