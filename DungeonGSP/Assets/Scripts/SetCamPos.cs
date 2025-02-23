using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamPos : MonoBehaviour
{
    public Transform campos;

    // Update is called once per frame
    void Update()
    {
        transform.position = campos.position;
    }
}
