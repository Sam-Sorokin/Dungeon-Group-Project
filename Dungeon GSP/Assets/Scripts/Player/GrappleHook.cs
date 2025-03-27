using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public Transform playerCamera;
    public CharacterController characterController;

    public int grappleRange = 500;
    public int grappleForce = 100;

    private Vector3 grapplePoint;
    public bool isGrappling = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryGrapple();
        }
        if (isGrappling)
        {
            GrappleMovement();
        }
    }

    void TryGrapple()
    {
        RaycastHit hit;
        Ray GrappleHookLocation = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(GrappleHookLocation, out hit, grappleRange))
        {
            Debug.Log("Grapple Detected Surface");
            grapplePoint = hit.point;
            isGrappling = true;
        }
    }

    void GrappleMovement()
    {
        Vector3 direction = (grapplePoint - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, grapplePoint);

        if (distance > 2f) // Stop grappling when close
        {
            characterController.Move(direction * grappleForce * Time.deltaTime);
        }
        else
        {
            isGrappling = false;
        }
    }
}
