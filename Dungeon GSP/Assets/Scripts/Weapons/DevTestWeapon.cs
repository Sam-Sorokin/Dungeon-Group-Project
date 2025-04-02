using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTestWeapon : WeaponBase
{
    protected int health;
    public int enemyHealth = 1;
    // Start is called before the first frame update
    void Start()
    {
        health = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
