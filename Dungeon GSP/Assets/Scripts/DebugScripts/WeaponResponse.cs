using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponResponse : MonoBehaviour
{
    public MeshRenderer mr;
    public WeaponBase weapon;
    // Start is called before the first frame update
    void Start()
    {
        mr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
