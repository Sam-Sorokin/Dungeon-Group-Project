using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class enemytracking : MonoBehaviour
{
    public Transform playertransform;
    private NavMeshAgent nma;
    public bool isattacking;
    public float speed;
    public float distanceFromPlayer;

    [Header("Tracking Distances")]
    public float patrolDistance = 5f;
    public float trackDistance = 3.2f;

    private void OnDrawGizmosSelected() // drawing the attack ranges to visualise the distance in the scene view
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, patrolDistance);
        Gizmos.DrawWireSphere(transform.position, trackDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("Player").transform;
        nma = GetComponent<NavMeshAgent>();
    }

    void HandleMovement()
    {
        distanceFromPlayer = Vector3.Distance(playertransform.position, transform.position);
        Vector3 directionToPlayer = (playertransform.position - transform.position).normalized;

        if(distanceFromPlayer <= patrolDistance && distanceFromPlayer >= trackDistance)
        {
            //Debug.Log("TrackingPlayer");
            nma.SetDestination(playertransform.position);
        }
        else if(distanceFromPlayer <= trackDistance)
        {
            nma.SetDestination(transform.position);
            // nav mesh agent stops tracking so we need to manually adjust rotation.
            // Rotate to look at the player
            Vector3 lookDirection = playertransform.position - transform.position;
            lookDirection.y = 0;
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // smoothenned rotation
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }
}
