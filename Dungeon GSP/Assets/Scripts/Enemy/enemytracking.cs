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
    public float distanceFromPlayer = 2f; // The distance you want the enemy to stay from the player


    // Start is called before the first frame update
    void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("Player").transform;
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playertransform != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 directionToPlayer = (playertransform.position - transform.position).normalized;

            // AI moves to player with an offset to avoid attempting to go inside the player
            Vector3 Destination = playertransform.position - directionToPlayer * distanceFromPlayer;

            nma.SetDestination(Destination);
        }

        //transform.position = Vector2.MoveTowards(transform.position, playertransform.position, speed * Time.deltaTime);
    }
}
