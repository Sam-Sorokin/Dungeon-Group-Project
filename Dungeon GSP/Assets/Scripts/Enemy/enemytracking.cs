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
    public float offsetAngle = 0f; // Angle to offset the position around the player (optional)


    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (playertransform != null)
        //{
        //    nma.SetDestination(playertransform.position);
        //}
        if (playertransform != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 directionToPlayer = (playertransform.position - transform.position).normalized;

            // Calculate a position near the player, offsetting by the desired distance
            Vector3 newDestination = playertransform.position - directionToPlayer * distanceFromPlayer;

            // You can also add an angle offset for variation if desired
            newDestination = Quaternion.Euler(0f, offsetAngle, 0f) * (newDestination - playertransform.position) + playertransform.position;

            // Set the destination to the new position
            nma.SetDestination(newDestination);
        }

        //transform.position = Vector2.MoveTowards(transform.position, playertransform.position, speed * Time.deltaTime);
    }
}
