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

    [Header("Tracking Distances")]
    public float patrolDistance = 5f;
    public float trackDistance = 3.2f;


    // Start is called before the first frame update
    void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("Player").transform;
        nma = GetComponent<NavMeshAgent>();
    }

    void HandleMovement()
    {
        float distanceFromPlayer = Vector3.Distance(playertransform.position, transform.position);
        Vector3 directionToPlayer = (playertransform.position - transform.position).normalized;

        if(distanceFromPlayer <= patrolDistance)
        {
            //Debug.Log("TrackingPlayer");
            Vector3 Destination = playertransform.position - directionToPlayer * trackDistance;
            nma.SetDestination(Destination);
        }
        else
        {
            nma.SetDestination(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }
}
