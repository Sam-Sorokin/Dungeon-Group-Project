using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : Grenade
{

    private void OnCollisionEnter(Collision collision)
    {
        Death();
    }
}
