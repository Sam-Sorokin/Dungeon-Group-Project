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


    void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.green;
            Vector3 start = playerCamera.position;
            Vector3 end = start + playerCamera.forward * grappleRange;
            Gizmos.DrawLine(start, end);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("Player Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryGrapple();
        }
        if (isGrappling && Input.GetKey(KeyCode.F))
        {
            GrappleMovement();
        }
    }

    bool canGrapple()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        return Physics.Raycast(ray, out RaycastHit hit, grappleRange);
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
