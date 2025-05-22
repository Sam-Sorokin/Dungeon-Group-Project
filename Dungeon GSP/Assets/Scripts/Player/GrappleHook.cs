using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public Transform playerCamera;
    public CharacterController characterController;
    public LineRenderer lineRenderer;
    public Transform grappleOriginPoint; // The point where the rope visually starts

    public int grappleRange = 500;
    public int grappleForce = 50;

    private Vector3 grapplePoint;
    public bool isGrappling = false;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("Player Camera").transform;
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component not found on this GameObject. Please add one.");
            enabled = false;
            return;
        }
        if (grappleOriginPoint == null)
        {
            // Fallback or warning if no origin point is set
            Debug.Log("Grapple Origin Point is not assigned. Line will originate from this GameObject's position. Consider creating and assigning an empty GameObject as the origin.");
            grappleOriginPoint = transform; // Default to this object's transform if not set
        }

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryGrapple();
        }

        if (isGrappling)
        {
            if (Input.GetKey(KeyCode.F))
            {
                GrappleMovement();
                DrawRope();
            }
            else
            {
                StopGrapple();
            }
        }
    }

    void TryGrapple()
    {
        RaycastHit hit;
        // Raycast still originates from the camera for aiming purposes
        Ray GrappleHookLocation = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(GrappleHookLocation, out hit, grappleRange))
        {
            Debug.Log("Grapple Detected Surface");
            grapplePoint = hit.point;
            isGrappling = true;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(1, grapplePoint); // End point of the line
        }
    }

    void GrappleMovement()
    {
        Vector3 direction = (grapplePoint - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, grapplePoint);

        if (distance > 2f)
        {
            characterController.Move(direction * grappleForce * Time.deltaTime);
        }
        else
        {
            StopGrapple();
        }
    }

    void DrawRope()
    {
        if (!lineRenderer.enabled) return;

        // Use the grappleOriginPoint for the start of the line
        lineRenderer.SetPosition(0, grappleOriginPoint.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }

    void StopGrapple()
    {
        isGrappling = false;
        lineRenderer.enabled = false;
    }
}