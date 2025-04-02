using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MannaManager : MonoBehaviour
{
    public UnityEvent outOfAmmo;
    public int ammo = 20;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(outOfAmmo != null && ammo <= 0)
        {
            outOfAmmo.Invoke();
        }
    }
}
